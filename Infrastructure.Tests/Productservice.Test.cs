using ProductApp.Infrastructure.Services;
using System;
using System.Linq;
using Xunit;

public class ProductServiceTests
{
    [Fact]
    public void AddProductToList_ShouldIncreaseCount_AndContainAddedProducts()
    {
        // Arrange
        var service = new ProductService(); 
        var beforeCount = service.GetProducts().Count();

        var name = "Test Product";
        var price = 199.90m;

        // Act
        service.AddProductToList(name, price);

        // Assert
        var after = service.GetProducts().ToList();

        Assert.Equal(beforeCount + 1, after.Count);

        var added = after.Last();
        Assert.Equal(name, added.ProductName);
        Assert.Equal(price, added.ProductPrice);
        Assert.NotEqual(Guid.Empty, added.ProductId);
    }
}