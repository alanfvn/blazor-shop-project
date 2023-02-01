using Microsoft.AspNetCore.Mvc;
using Shop.Online.Api.Extensions;
using Shop.Online.Api.Repositories.Contracts;
using ShopOnlineModels.Dtos;

namespace Shop.Online.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository rep) {
            this.productRepository = rep;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems() {
            try {
                var products = await productRepository.GetItems();
                var categories = await productRepository.GetCategories();

                if (products == null || categories == null) {
                    return NotFound();
                } else {
                    var productsDto = products.ConvertToDto(categories);
                    return Ok(productsDto);
                }
            } catch (Exception) {
                return StatusCode(500, "Error retrieving data from the database");
            }
        }
    }
}
