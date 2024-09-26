using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using MicroservicesWithCQRSDesignPattern.Queries.CommandModel;
using MicroservicesWithCQRSDesignPattern.Queries.QueryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesWithCQRSDesignPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryHandler<GetProductsQuery, IEnumerable<GetAllProductsCommand>> _getProductsQueryHandler;
        private readonly ICommandHandler<CreateProductCommand> _createProductCommandHandler;
        private readonly ICommandHandler<UpdateProductCommand> _updateProductCommandHandler;
        private readonly ICommandHandler<DeleteProductCommand> _deleteProductCommandHandler;

        public ProductController(
            IQueryHandler<GetProductsQuery, IEnumerable<GetAllProductsCommand>> getProductsQueryHandler,
            ICommandHandler<CreateProductCommand> createProductCommandHandler,
            ICommandHandler<UpdateProductCommand> updateProductCommandHandler,
            ICommandHandler<DeleteProductCommand> deleteProductCommandHandler
            )
        {
            _getProductsQueryHandler = getProductsQueryHandler;
            _createProductCommandHandler = createProductCommandHandler;
            _updateProductCommandHandler = updateProductCommandHandler;
            _deleteProductCommandHandler = deleteProductCommandHandler;
        }

        [HttpGet(nameof(GetProducts))]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _getProductsQueryHandler.Handle(new GetProductsQuery());
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(CreateProduct))]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            try
            {
                await _createProductCommandHandler.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(nameof(UpdateProduct))]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            try
            {
                await _updateProductCommandHandler.Handle(command);
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating product: {ex.Message}");
            }
        }

        [HttpDelete(nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                var command = new DeleteProductCommand { Id = productId };
                await _deleteProductCommandHandler.Handle(command);
                return Ok("Product deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting product: {ex.Message}");
            }
        }
    }
}
