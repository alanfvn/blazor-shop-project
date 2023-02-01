using Shop.Online.Api.Entities;

namespace Shop.Online.Api.Repositories.Contracts {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);
    }
}
