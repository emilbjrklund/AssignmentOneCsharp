using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductApp.Infrastructure.Interfaces;
using ProductApp.Infrastructure.Services;
using ProductApp.Services;
using System.Globalization;
using System.Text.Json;

string filePath = @"c:\data\products.json";

var builder = Host.CreateApplicationBuilder();
builder.Services.AddSingleton<IFileService>(_ => new JsonFileService(filePath));
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<MenuServices>();

var app = builder.Build();

app.Services.GetRequiredService<MenuServices>().DisplayMainMenu();
