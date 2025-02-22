using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TheGrace.Application.Services.Product;
using TheGrace.Application.Services.TimeZone;
using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Contract.Services.Product;
using TheGrace.Domain.Abstractions;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Enumerations;
using TheGrace.Persistence;
using Model = TheGrace.Domain.Entities;

namespace TheGrace.Application.UseCases.V1.Commands.Product;
internal sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand, Response.ProductResponse>
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IRepositoryBase<Model.ProductCategory, int> _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeZoneService _timeZoneService;
    private readonly IProductService _productService;
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(
        IRepositoryBase<Model.Product, int> productRepository,
        IRepositoryBase<Model.ProductCategory, int> productCategoryRepository,
        IUnitOfWork unitOfWork,
        ITimeZoneService timeZoneService,
        IPublisher publisher,
        IProductService productService
        )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _unitOfWork = unitOfWork;
        _timeZoneService = timeZoneService;
        _productService = productService;
        _publisher = publisher;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _productCategoryRepository.FindByIdAsync(request.productCategoryId);
        if (category is null)
            throw new InvalidDataException("ProductCategory could not found!");

        DateTimeOffset createdAt = _timeZoneService.GetCurrentTime();

        var random = new Random(); // Single Random instance

        var productCode = _productService.CreateProductCode(category.Name, random.Next(1, 99));
        var categorieImages = _productService.GetProductImages();
        string image = _productService.GetProductImageSingle(categorieImages, category.Name);
        string createdBy = "Candy";

        var itemProduct = new Model.Builder.ProductBuilderPattern.ProductBuilder()
                             .SetType(StatusEnum.Active)
                             .SetName(request.name)
                             .SetCode(productCode)
                             .SetQuantity(Math.Max(request.quantity, 1))
                             .SetPrice(Math.Max(request.price, 100000))
                             .SetSoftDelete(true)
                             .SetDescription(request.description ?? "...")
                             .SetImage(image)
                             .SetCreatedBy(createdBy, createdAt)
                             .SetUpdatedBy(createdBy, createdAt)
                             .SetProductCategory(category)
                             .Build();

        _productRepository.Add(itemProduct);

        await _unitOfWork.CommitAsync();
        int productId = itemProduct.Id;

        await _publisher.Publish(new DomainEvent.ProductChangedEvent(createdBy, productId));

        var product = await _productService.GetProductById(productId);

        return Result.Success(product);
    }
}
