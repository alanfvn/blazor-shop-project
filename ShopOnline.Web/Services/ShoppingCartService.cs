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

        public async Task<List<CartItemDto>> GetItems(int userID) {
            try {
                var resp = await httpClient.GetAsync($"api/ShoppingCart/{userID}/GetItems");
                if (resp.IsSuccessStatusCode) {
                    if(resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return Enumerable.Empty<CartItemDto>().ToList();
                    }
                    return await resp.Content.ReadFromJsonAsync<List<CartItemDto>>();
                } else {
                    var msg = await resp.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {resp.StatusCode}, Message: {msg}");
                }

            } catch (Exception ex) {
                throw;
            }
        }

        public async Task<CartItemDto> DeleteItem(int id) {
            try {
                var resp = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");
                if (resp.IsSuccessStatusCode) {
                    return await resp.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return default; 
            } catch (Exception ex) {

                throw;
            }
        }
    }
}
