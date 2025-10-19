using Microsoft.Extensions.Options;
using ProductApp.Infrastructure.Interfaces;
using ProductApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductApp.Services;

public class MenuServices(IProductService productService, IFileService fileService, string filePath = @"c:\data\products.json")
{
    private readonly IProductService _productService = productService;
    private readonly IFileService _fileService = fileService;

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true
    };

    public void DisplayMainMenu()
    {
        var running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("\n ------ MAIN MENU ------");
            Console.WriteLine("\n 1. Add New Product ");
            Console.WriteLine("\n 2. List All Products ");
            Console.WriteLine("\n 3. Save Products To File ");
            Console.WriteLine("\n 4. Import Products From File ");
            Console.WriteLine("\n Q. Quit Menu ");
            Console.Write("\n Choose Option: ");

            var option = Console.ReadLine().ToUpperInvariant();

            switch (option)
            {
                case "1":
                    AddProductOption();
                    break;

                case "2":
                    ListAllOption();
                    break;

                case "3":
                    SaveToJsonFileOption();
                    break;

                case "4":
                    ImportFromFileOption();
                    break;

                case "Q":
                    running = false;
                    break;

                default:
                    break;
            }
        }
    }

    public void AddProductOption()
    {
        Console.Clear();

        Console.Write("\nName: ");
        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Error, Product name is required");
            return;
        }

        Console.Write("\nPrice: ");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            Console.WriteLine("Invalid Price...");
            return;
        }

        _productService.AddProductToList(name, price);
        Console.WriteLine("\nProduct Added.");
        Console.ReadKey();
    }

    public void ListAllOption()
    {
        var allProducts = _productService.GetProducts().ToList();

        Console.Clear();

        if (allProducts.Count == 0)
        {
            Console.WriteLine("No Products Found:");
        }

        foreach (var product in allProducts)
        {
            Console.WriteLine($" {product.ProductName} \t{product.ProductPrice}kr \t{product.ProductId}");
        }

        Console.ReadKey();
    }

    public void SaveToJsonFileOption()
    {
        var products = _productService.GetProducts();
        var json = JsonSerializer.Serialize(products);
        _fileService.SaveToFile(json);
    }

    public void ImportFromFileOption()
    {
        Console.Clear();

        try
        {
            var content = _fileService.GetContentFromFile();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("No products found in file.");
                Console.ReadKey();
                return;
            }

            var loadedProducts = JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions) ?? new List<Product>();

            if (loadedProducts.Count == 0)
            {
                Console.WriteLine("File contained no products.");
                Console.ReadKey();
                return;
            }

            foreach (var product in loadedProducts)
            {
                _productService.AddProductToList(product.ProductName, product.ProductPrice);
            }

            Console.WriteLine($"Imported {loadedProducts.Count} products.");
        }


        catch (Exception ex)
        {
            Console.WriteLine($"Import failed: {ex.Message}");
        }

        Console.ReadKey();
    }
}
