using Shop.Online.Api.Entities;
using ShopOnlineModels.Dtos;

namespace Shop.Online.Api.Extensions {
    public static class DtoConversions {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products,
            IEnumerable<ProductCategory> productCategories) {
            /*
              Create a new ProductDto class based on the blueprints of the database,
              so we have the category name inside the ProductDto class.
             */
            return (
                from product in products
                join productCategory in productCategories
                on product.CategoryId equals productCategory.Id

                select new ProductDto {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageURL = product.ImageURL,
                    Price = product.Price,
                    Qty = product.Qty,
                    CategoryId = product.CategoryId,
                    CategoryName = productCategory.Name
                }).ToList();
        }

        public static ProductDto ConvertToDto(this Product product, ProductCategory category) {
            return new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.CategoryId,
                CategoryName = category.Name
            };
        }
    }
}
