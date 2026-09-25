using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.DTOs.Products
{

    public sealed record CreateProductRequest(
        string Name,
        string? Description,
        decimal Price,
        int StockQuantity);
}
