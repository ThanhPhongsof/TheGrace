using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TheGrace.Contract.Hubs;

public class ProductHub : Hub
{
    public async Task NotifyProductAdded(string productId)
    {
        await Clients.All.SendAsync("ProductAdded", productId);
    }

    public async Task NotifyProductUpdated(string productId)
    {
        await Clients.All.SendAsync("ProductUpdated", productId);
    }
}
