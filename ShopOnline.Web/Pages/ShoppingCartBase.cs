using Microsoft.AspNetCore.Components;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ShoppingCartBase:ComponentBase {

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync() {
            try {
                //we retrieve the items stored in the cart of the hardcoded user that we defined.
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            } catch (Exception ex) {
                ErrorMessage = ex.Message;
                throw;
            }
        }


        protected async Task DeleteCartItem_Click(int id) {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            //refresh the ui
            RemoveCartItem(id);
        }

        private CartItemDto GetCartItemDto(int id) {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
        private void RemoveCartItem(int id) {
            var cartItemDto = GetCartItemDto(id);
            ShoppingCartItems.Remove(cartItemDto);
        }
    }
}
