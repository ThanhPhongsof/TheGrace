using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Contract.Abstractions;

namespace TheGrace.Contract.Services.Product;

public class DomainEvent
{
    public record ProductChangedEvent(string ReceiverId, int Id = 0) : IDomainEvent;
}
