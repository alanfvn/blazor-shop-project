using ShopOnline.Models.Dtos;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Services.Contracts {
    public interface IProductService {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<IEnumerable<ProductDto>> GetItemsByCategory(int id);
    }
}
