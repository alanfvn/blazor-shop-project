using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Services.Contracts {
    public interface IShoppingCartService {
        Task<List<CartItemDto>> GetItems(int userID);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);
        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);
    }
}
