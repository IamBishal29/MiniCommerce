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

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
        var products =
            await _productRepository.GetAllAsync(cancellationToken);

        return products
            .Select(MapToDto)
            .ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetByIdAsync(
                id,
                cancellationToken);

        return product is null
            ? null
            : MapToDto(product);
    }

    public async Task<ProductDto?> UpdateAsync(
    Guid id,
    UpdateProductRequest request,
    CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (product is null)
            return null;

        product.UpdateDetails(
            request.Name,
            request.Description,
            request.Price);

        await _productRepository.UpdateAsync(
            product,
            cancellationToken);

        return MapToDto(product);
    }

    public async Task<ProductDto?> UpdateStockAsync(
    Guid id,
    UpdateStockRequest request,
    CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (product is null)
            return null;

        product.UpdateStock(request.StockQuantity);

        await _productRepository.UpdateAsync(
            product,
            cancellationToken);

        return MapToDto(product);
    }

    public async Task<bool> DeleteAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (product is null)
            return false;

        await _productRepository.DeleteAsync(
            product,
            cancellationToken);

        return true;
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