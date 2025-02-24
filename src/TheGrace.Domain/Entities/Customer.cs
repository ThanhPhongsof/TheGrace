using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TheGrace.Domain.Entities.EntityBase;
using TheGrace.Domain.Enumerations;

namespace TheGrace.Domain.Entities;

public class Customer : EntityAudit<int>
{
    public int Type { get; private set; }

    [Column(TypeName = "nvarchar(512)")]
    [MaxLength(4000)]
    [Required]
    public string Name { get; private set; }

    [Column(TypeName = "varchar(20)")]
    [MaxLength(20)]
    [Required]
    public string PhonePrimary { get; private set; }

    [Column(TypeName = "varchar(20)")]
    [MaxLength(20)]
    public string PhoneSecond { get; private set; }

    [Column(TypeName = "nvarchar(4000)")]
    [MaxLength(4000)]
    [Required]
    public string Address { get; private set; }

    public virtual ICollection<Order>? Orders { get; private set; }

    public Customer() { }

    public Customer(int type, string name,
                    string phonePrimary, string phoneSecond,
                    string address, bool isInActive,
                    string createdBy, DateTimeOffset createdAt, string updatedBy, DateTimeOffset updatedAt,
                    int? id)
    {
        Id = id ?? 0;
        Type = type;
        Name = name;
        PhonePrimary = phonePrimary;
        PhoneSecond = phoneSecond;
        Address = address;
        IsInActive = isInActive;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
    }

    public void Update(string name, string phonePrimary, string phoneSecond, string address, 
                       string updatedBy, DateTimeOffset updatedAt)
    {
        Name = name;
        PhonePrimary = phonePrimary;
        PhoneSecond = phoneSecond;
        Address = address;
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}
