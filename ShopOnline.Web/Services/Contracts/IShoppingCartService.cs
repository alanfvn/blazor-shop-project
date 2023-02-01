using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Services.Contracts {
    public interface IShoppingCartService {
        Task<List<CartItemDto>> GetItems(int userID);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItem(int id);
    }
}
