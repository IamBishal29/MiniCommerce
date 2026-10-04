using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.DTOs.Products
{
    public sealed record UpdateProductRequest(
        string Name,
        string? Description,
        decimal Price);
}
