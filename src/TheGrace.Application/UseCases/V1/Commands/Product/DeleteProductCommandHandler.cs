using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Contract.Abstractions.Shared;
using TheGrace.Contract.Services.Product;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Abstractions;
using Model = TheGrace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TheGrace.Persistence;
using TheGrace.Application.Services.TimeZone;
using MediatR;

namespace TheGrace.Application.UseCases.V1.Commands.Product;
internal sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly ITimeZoneService _timeZoneService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(ApplicationDbContext context, IUnitOfWork unitOfWork, ITimeZoneService timeZoneService, IPublisher publisher)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _timeZoneService = timeZoneService;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _context.Products
                    .Where(p => p.Id == request.id)
                    .ExecuteUpdateAsync(upt =>
                        upt.SetProperty(b => b.IsInActive, false)
                           .SetProperty(b => b.UpdatedAt, _timeZoneService.GetCurrentTime())
                           .SetProperty(b => b.UpdatedBy, "Kang"));

        await _unitOfWork.CommitAsync();

        await _publisher.Publish(new DomainEvent.ProductChangedEvent("Kang", request.id));

        return Result.Success();
    }
}
