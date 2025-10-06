namespace ProductApp.Infrastructure.Models;

public class Product
{
    public Guid ProductId { get; init; } = Guid.NewGuid();

    public string ProductName { get; set; } = null!;

    public decimal ProductPrice { get; set; }
}

