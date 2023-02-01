using Microsoft.AspNetCore.Components;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class ProductsBase: ComponentBase {
        [Inject]
        public IProductService ProductService { get; set; } 

        public IEnumerable<ProductDto> Products { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync() {
            try {
                Products = await ProductService.GetItems();
            } catch (Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory() {
            return from product in Products group product by product.CategoryId into prodByCatGroup orderby prodByCatGroup.Key select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDto) {
            return groupedProductDto.FirstOrDefault(pg => pg.CategoryId == groupedProductDto.Key).CategoryName;
        }
    }
}
