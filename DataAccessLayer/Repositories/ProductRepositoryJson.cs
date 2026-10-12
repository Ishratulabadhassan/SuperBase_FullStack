using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Models;

namespace DataAccessLayer.Repositories
public class ProductRepositoryJson : IProductRepository
{
    private readonly string _filePath;

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public ProductRepositoryJson(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "products.json");
    }

    private async Task<List<ProductModel>> ReadProductsAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<ProductModel>();
        }

        string json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<ProductModel>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<ProductModel>>(
                json,
                _options) ?? new List<ProductModel>();
        }
        catch (JsonException)
        {
            throw new InvalidOperationException(
                "The products.json file contains invalid JSON.");
        }
    }

    private async Task WriteProductsAsync(
        List<ProductModel> products)
    {
        string json = JsonSerializer.Serialize(
            products,
            _options);

        await File.WriteAllTextAsync(
            _filePath,
            json);
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        return await ReadProductsAsync();
    }

    public async Task<ProductModel?> GetByIdAsync(Guid id)
    {
        var products = await ReadProductsAsync();

        return products.FirstOrDefault(
            x => x.Id == id);
    }

    public async Task AddAsync(ProductModel product)
    {
        var products = await ReadProductsAsync();

        products.Add(product);

        await WriteProductsAsync(products);
    }

    public async Task UpdateAsync(ProductModel product)
    {
        var products = await ReadProductsAsync();

        var existingProduct = products.FirstOrDefault(
            x => x.Id == product.Id);

        if (existingProduct == null)
        {
            return;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Category = product.Category;

        await WriteProductsAsync(products);
    }

    public async Task DeleteAsync(Guid id)
    {
        var products = await ReadProductsAsync();

        var product = products.FirstOrDefault(
            x => x.Id == id);

        if (product == null)
        {
            return;
        }

        products.Remove(product);

        await WriteProductsAsync(products);
    }

    public async Task<List<ProductModel>> SearchAsync(
        string keyword)
    {
        var products = await ReadProductsAsync();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            return products;
        }

        return products
            .Where(x =>
                x.Name.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase)
                ||
                x.Category.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}