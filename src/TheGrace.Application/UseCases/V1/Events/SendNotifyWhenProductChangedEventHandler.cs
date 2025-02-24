using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TheGrace.Contract.Abstractions;
using TheGrace.Contract.Hubs;
using TheGrace.Contract.Services.Product;

namespace TheGrace.Application.UseCases.V1.Events;
internal sealed class SendNotifyWhenProductChangedEventHandler : IDomainEventHandler<DomainEvent.ProductChangedEvent>
{
    private readonly IHubContext<ProductHub> _hubContext;

    public SendNotifyWhenProductChangedEventHandler(IHubContext<ProductHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(DomainEvent.ProductChangedEvent notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("ProductChanged", notification.Id, "Change");
    }
}
