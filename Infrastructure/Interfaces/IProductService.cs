using ProductApp.Infrastructure.Models;
namespace ProductApp.Infrastructure.Interfaces;
public interface IProductService
{
    public bool AddProductToList(string productName, decimal productPrice);

    IEnumerable<Product> GetProducts();
}
