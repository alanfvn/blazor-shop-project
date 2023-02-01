using Microsoft.AspNetCore.Components;
using ShopOnline.Web.Services;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ProductsDetailsBase:ComponentBase {

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ProductDto Product { get; set; } 
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync() {
            try {
                Product = await ProductService.GetItem(Id);

            } catch (Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto) {
            try {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
                NavigationManager.NavigateTo("/ShoppingCart");
            } catch (Exception) {
                throw;
            }
        }

    }
}
