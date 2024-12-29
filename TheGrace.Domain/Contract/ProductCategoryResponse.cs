namespace TheGrace.Domain.Contract;

public class ProductCategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Type { get; set; }

    public bool IsInActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
