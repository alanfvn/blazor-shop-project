using Microsoft.AspNetCore.Components;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ProductsBase: ComponentBase {
        [Inject]
        public IProductService ProductService { get; set; } 

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync() {
            Products = await ProductService.GetItems();
        }


    }
}
