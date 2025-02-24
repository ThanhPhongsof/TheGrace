using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.EntityBase;

namespace TheGrace.Domain.Entities;

public class OrderLog : Entity<int>
{
    public int OrderId { get; private set; }

    [Required]
    public int Status { get; private set; }

    [Column(TypeName = "varchar(128)")]
    [MaxLength(128)]
    [Required]
    public string StatusNote { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; private set; }

    [Required]
    public string CreatedBy { get; private set; }

    public virtual Order Order { get; private set; }

    public OrderLog() { }

    public OrderLog(
        int status, string statusNote, 
        DateTimeOffset createdAt, string createdBy, Order order, int? id
        )
    {
        Id = id ?? 0;
        Status = Status;
        StatusNote = statusNote;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        OrderId = order.Id;
    }
}
