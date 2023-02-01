using Microsoft.EntityFrameworkCore;
using Shop.Online.Api.Data;
using Shop.Online.Api.Entities;
using Shop.Online.Api.Repositories.Contracts;

namespace Shop.Online.Api.Repositories {
    public class ProductRepository : IProductRepository {

        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext) {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories() {
            var categories = await shopOnlineDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id) {
            var category = await shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(category=> category.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id) {
            var product = await shopOnlineDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems() {
            var products = await this.shopOnlineDbContext.Products.ToListAsync();
            return products;
        }
    }
}
