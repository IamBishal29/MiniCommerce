using ProductService.Application.Abstractions.Persistence;
using ProductService.Application.DTOs.Products;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Services;

public sealed class ProductAppService
{
    private readonly IProductRepository _productRepository;

    public ProductAppService(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var exists = await _productRepository.ExistsByNameAsync(
            request.Name,
            cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException(
                $"A product named '{request.Name}' already exists.");
        }

        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity);

        await _productRepository.AddAsync(
            product,
            cancellationToken);

        return MapToDto(product);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CreatedAt,
            product.UpdatedAt);
    }
}