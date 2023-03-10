using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using ShopOnlineModels.Dtos;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services {
    public class ProductService: IProductService{

        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetItems() {
            try {
                var resp = await this.httpClient.GetAsync("api/Product");
                if (resp.IsSuccessStatusCode) {
                    if(resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return default;
                    } else {
                        return await resp.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                    }
                }else {
                    var message = await resp.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            } catch (Exception) {
                throw;
            }
        }

        public async Task<ProductDto> GetItem(int id) {
            try {
                var resp = await httpClient.GetAsync($"api/Product/{id}");
                if (resp.IsSuccessStatusCode) {
                    if(resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return default;
                    }
                    return await resp.Content.ReadFromJsonAsync<ProductDto>();
                } else {
                    var message = await resp.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            } catch (Exception) {
                throw;
            }
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories() {
            try {
                var resp = await httpClient.GetAsync("/api/Product/GetProductCategories");
                if (resp.IsSuccessStatusCode) {
                    if (resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return default;
                    }
                    return await resp.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
                } else {
                    var msg = await resp.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {resp.StatusCode}, Message: {msg}");
                }
            } catch (Exception) {
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetItemsByCategory(int id) {
            try {
                var resp = await httpClient.GetAsync($"/api/Product/{id}/GetItemsByCategory");
                if (resp.IsSuccessStatusCode) {
                    if (resp.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return default;
                    }
                    return await resp.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                } else {
                    var msg = await resp.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {resp.StatusCode}, Message: {msg}");
                }
            } catch (Exception) {
                throw;
            }
        }

    }
}
