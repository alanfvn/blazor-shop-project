using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Services.Contracts {
    public interface IShoppingCartService {
        Task<IEnumerable<CartItemDto>> GetItems(int userID);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
    }
}
