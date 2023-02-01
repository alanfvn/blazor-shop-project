using Microsoft.AspNetCore.Components;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ProductsByCategoryBase:ComponentBase {
        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductDto>  Products { get; set; }

        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        protected async override Task OnParametersSetAsync() {
            try {
                Products = await ProductService.GetItemsByCategory(CategoryId);
                if(Products != null && Products.Count() > 0) {
                    var productDto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);
                    if(productDto != null) {
                        CategoryName = productDto.CategoryName;
                    }
                }
            } catch (Exception ex) {
                ErrorMessage = ex.Message;
                throw;
            }
        }

    }
}
