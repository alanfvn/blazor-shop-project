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

        public Task<ProductCategory> GetCategory(int id) {
            throw new NotImplementedException();
        }

        public Task<Product> GetItem(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems() {
            var products = await this.shopOnlineDbContext.Products.ToListAsync();
            return products;
        }
    }
}
