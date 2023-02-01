using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services {
    public class ShoppingCartService : IShoppingCartService {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }
        public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto) {
            try {
                var resp = await httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart/", cartItemToAddDto);
                if (resp.IsSuccessStatusCode) {
                    if(resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return default;
                    }
                    return await resp.Content.ReadFromJsonAsync<CartItemDto>();
                } else {
                    var msg = await resp.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {resp.StatusCode}, Message: {msg}");
                }
            } catch (Exception) {
                throw;
            }
        }

        public async Task<IEnumerable<CartItemDto>> GetItems(int userID) {
            try {
                var resp = await httpClient.GetAsync($"api/ShoppingCart/{userID}/GetItems");
                if (resp.IsSuccessStatusCode) {
                    if(resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return Enumerable.Empty<CartItemDto>();
                    }
                    return await resp.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
                } else {
                    var msg = await resp.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {resp.StatusCode}, Message: {msg}");
                }

            } catch (Exception ex) {
                throw;
            }
        }
    }
}
