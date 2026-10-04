using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs.Products;
using ProductService.Application.Services;

namespace ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly ProductAppService _productAppService;

        public ProductsController(
            ProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            var products =
                await _productAppService.GetAllAsync(
                    cancellationToken);

            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var product =
                await _productAppService.GetByIdAsync(
                    id,
                    cancellationToken);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(
            CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var product =
                await _productAppService.CreateAsync(
                    request,
                    cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Update(
            Guid id,
            UpdateProductRequest request,
            CancellationToken cancellationToken)
        {
            var product =
                await _productAppService.UpdateAsync(
                    id,
                    request,
                    cancellationToken);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPatch("{id:guid}/stock")]
        public async Task<ActionResult<ProductDto>> UpdateStock(
            Guid id,
            UpdateStockRequest request,
            CancellationToken cancellationToken)
        {
            var product =
                await _productAppService.UpdateStockAsync(
                    id,
                    request,
                    cancellationToken);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deleted =
                await _productAppService.DeleteAsync(
                    id,
                    cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}