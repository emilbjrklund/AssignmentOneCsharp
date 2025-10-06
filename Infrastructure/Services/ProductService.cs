using ProductApp.Infrastructure.Interfaces;
using ProductApp.Infrastructure.Models;

namespace ProductApp.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = [];
    public bool AddProductToList(string productName, decimal productPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
            return false;

        if (productPrice < 0)
            return false;

        _products.Add(new Product
        {
            ProductName = productName,
            ProductPrice = productPrice,
        });
        return true;
    }

    public IEnumerable<Product> GetProducts() => _products;
}
