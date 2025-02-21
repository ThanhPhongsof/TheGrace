using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TheGrace.Application.Hubs;

namespace TheGrace.Application.Services.Notification;
public class RealtimeNotificationService
{
    private readonly IHubContext<RealtimeHub> _hubContext;

    public RealtimeNotificationService(IHubContext<RealtimeHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyClientAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }
}
