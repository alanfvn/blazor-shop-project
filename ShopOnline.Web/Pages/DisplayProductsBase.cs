using Microsoft.AspNetCore.Components;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Web.Pages {
    public class DisplayProductsBase:ComponentBase{

        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }

    }
}
