using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging.Abstractions;
using Shop.Online.Api.Data;
using Shop.Online.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnlineModels.Dtos;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }
        private async Task<bool> CartItemExists(int cartId, int prodId)
        {
            return await shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == prodId);
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            var itemExists = await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId);

            if (itemExists)
            {
                //the item already exists.
                return null;
            }

            var item = await (from product in shopOnlineDbContext.Products
                              where product.Id == cartItemToAddDto.ProductId
                              select new CartItem
                              {
                                  CartId = cartItemToAddDto.CartId,
                                  ProductId = product.Id,
                                  Qty = cartItemToAddDto.Qty,
                              }
                              ).SingleOrDefaultAsync();
            if (item == null)
            {
                return null;
            }
            var result = await shopOnlineDbContext.CartItems.AddAsync(item);
            await shopOnlineDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int id)
        {
            var val = await (from cart in shopOnlineDbContext.Carts
                             join cartItem in shopOnlineDbContext.CartItems
                             on cart.Id equals cartItem.Id
                             where cartItem.Id == id
                             select new CartItem
                             {
                                 Id = cartItem.Id,
                                 ProductId = cartItem.ProductId,
                                 Qty = cartItem.Qty,
                                 CartId = cartItem.CartId,
                             }).SingleOrDefaultAsync();
            return val;
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userid)
        {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userid 
                          select new CartItem {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
