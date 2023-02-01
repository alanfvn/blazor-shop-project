using Microsoft.AspNetCore.Components;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ShoppingCartBase:ComponentBase {

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
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


    }
}
