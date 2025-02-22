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
internal sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand, Response.ProductResponse>
{
    private readonly IRepositoryBase<Model.Product, int> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITimeZoneService _timeZoneService;
    private readonly IProductService _productService;
    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(
        IRepositoryBase<Model.Product, int> productRepository,
        IUnitOfWork unitOfWork,
        ITimeZoneService timeZoneService,
        IProductService productService,
        IPublisher publisher
        )
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _timeZoneService = timeZoneService;
        _productService = productService;
        _publisher = publisher;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.id);

        DateTimeOffset createdAt = _timeZoneService.GetCurrentTime();

        string updatedBy = "Arvix";
        string empty = "...";

        product.Update(
            request.type,
            request.name ?? empty,
            request.description ?? empty,
            Math.Max(request.price, 100000),
            Math.Max(request.quantity, 1),
            updatedBy,
            createdAt);
        _productRepository.Update(product);

        await _unitOfWork.CommitAsync();

        await _publisher.Publish(new DomainEvent.ProductChangedEvent(updatedBy, product.Id));

        var productUpdated = await _productService.GetProductById(product.Id);

        return Result.Success(productUpdated);
    }
}
