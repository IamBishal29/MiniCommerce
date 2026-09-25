using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.DTOs.Products
{
    public sealed record ProductDto(
        Guid Id,
        string Name,
        string? Description,
        decimal Price,
        int StockQuantity,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
