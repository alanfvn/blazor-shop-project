using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ShoppingCartBase:ComponentBase {

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }
        public string TotalPrice { get; set; }
        public string TotalQty { get; set; }    

        protected override async Task OnInitializedAsync() {
            try {
                //we retrieve the items stored in the cart of the hardcoded user that we defined.
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummaryTotals();
            } catch (Exception ex) {
                ErrorMessage = ex.Message;
                throw;
            }
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty) {

            try {
                if(qty >0) {
                    var updateItemDto = new CartItemQtyUpdateDto {
                        CartItemId = id,
                        Qty = qty
                    };
                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQty(updateItemDto);
                    UpdateItemTotalPrice(returnedUpdateItemDto);
                    await MakeUpdateQtyButtonVisible(id, false);
                } else {
                    var item = ShoppingCartItems.FirstOrDefault(x => x.Id == id);

                    if(item != null) {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
                CalculateCartSummaryTotals();
            } catch (Exception) {

                throw;
            }
        }

        protected async Task DeleteCartItem_Click(int id) {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            //refresh the ui
            RemoveCartItem(id);
            CalculateCartSummaryTotals();
        }

        private CartItemDto GetCartItemDto(int id) {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
        private void RemoveCartItem(int id) {
            var cartItemDto = GetCartItemDto(id);
            ShoppingCartItems.Remove(cartItemDto);
        }

        private void CalculateCartSummaryTotals() {
            SetTotalPrice();
            SetTotalQty();
        }

        private void SetTotalPrice() {
            TotalPrice = ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        }

        private void SetTotalQty() { 
            TotalQty = ShoppingCartItems.Sum(p => p.Qty).ToString();
        }
        
        private void UpdateItemTotalPrice(CartItemDto cartItemDto) {
            var item = GetCartItemDto(cartItemDto.Id);
            if(item != null) {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }
        }

        protected async Task MakeUpdateQtyButtonVisible(int id, bool visible) {
            await JSRuntime.InvokeVoidAsync("MakeUpdateQtyVisible", id, visible);
        }

        protected async Task UpdateQty_Input(int id) {
            await MakeUpdateQtyButtonVisible(id, true);
        }
    }
}
