using Microsoft.AspNetCore.Mvc;
using Shop.Online.Api.Extensions;
using Shop.Online.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id) {
            try {

                var product = await productRepository.GetItem(id);
                if (product == null) {
                    return BadRequest();
                } else {
                    var category = await this.productRepository.GetCategory(product.CategoryId);
                    var finalProd = product.ConvertToDto(category);
                    return Ok(finalProd);
                }
            } catch (Exception) {
                return StatusCode(500, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories() {
            try {
                var categories = await productRepository.GetCategories();
                if(categories == null) {
                    return NotFound();
                }
                return Ok(categories.ConvertToDto());
            } catch (Exception) {
                return StatusCode(500, "error retrieving data from the database");
            }
        }
        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId) {
            try {
                var products = await productRepository.GetItemsByCategory(categoryId);
                var productCategories = await productRepository.GetCategories();
                var productDtos = products.ConvertToDto(productCategories);

                return Ok(productDtos);

            } catch (Exception) {
                return StatusCode(500,
                                "Error retrieving data from the database");
            }
        }
    }
}
