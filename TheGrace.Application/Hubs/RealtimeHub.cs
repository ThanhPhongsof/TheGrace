using Microsoft.AspNetCore.SignalR;

namespace TheGrace.Application.Hubs;

public class RealtimeHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
