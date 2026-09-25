using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public int StockQuantity { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }


    private Product()
    {
    }


    public Product(
        string name,
        string? description,
        decimal price,
        int stockQuantity)
    {
        ValidateName(name);
        ValidatePrice(price);
        ValidateStockQuantity(stockQuantity);

        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description?.Trim();
        Price = price;
        StockQuantity = stockQuantity;
        CreatedAt = DateTime.UtcNow;
    }


    public void UpdateDetails(
        string name,
        string? description,
        decimal price)
    {
        ValidateName(name);
        ValidatePrice(price);

        Name = name.Trim();
        Description = description?.Trim();
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }


    public void UpdateStock(int quantity)
    {
        ValidateStockQuantity(quantity);

        StockQuantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }


    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Product name is required.",
                nameof(name));
    }


    private static void ValidatePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(price),
                "Product price must be greater than zero.");
    }


    private static void ValidateStockQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "Stock quantity cannot be negative.");
    }
}