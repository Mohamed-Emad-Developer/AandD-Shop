using ECommerceMS.Data;
using ECommerceMS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMS.services.repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDB DBContext;
        public CartRepository(ECommerceDB _DBContext)
        {
            DBContext = _DBContext;
        }
        public Cart Get(int id)
        {
            return DBContext.Carts.Include(x => x.ProductCarts).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);
        }
        public int Create(string customerId)
        {
            var cart = new Cart() { CustomerId = customerId };
            DBContext.Carts.Add(cart);
            return DBContext.SaveChanges();
        }
        public int AddPrpductToCart(int prodcutId, string customerId)
        {
            var cartInDb = DBContext.Carts.FirstOrDefault(c => c.CustomerId == customerId);
            var productInDb = DBContext.Products.FirstOrDefault(p => p.Id == prodcutId);
            var prodcutCartInDB = DBContext.ProductCarts.Where(pc => pc.ProductId == prodcutId && pc.CartId == cartInDb.Id);
            if (!(prodcutCartInDB.Count() > 0))
            {
                var prodcutCart = new ProductCarts() { Quantity = 1, ProductId = prodcutId, CartId = cartInDb.Id };

                productInDb.ProductCarts.Add(prodcutCart);
                cartInDb.ProductCarts.Add(prodcutCart);
            }
            return DBContext.SaveChanges();
        }



        public Cart GetByUserId(string customerId)
        {
            var cartInDb = DBContext.Carts.Include(c => c.ProductCarts).ThenInclude(pc => pc.Product).FirstOrDefault(c => c.CustomerId == customerId);
            return cartInDb;
        }

        public void IncrementQuantity(int cartId)
        {
            var productCartInDb = DBContext.ProductCarts.Include(pc=>pc.Product).FirstOrDefault(pc => pc.CartId == cartId);
            if (productCartInDb.Quantity < productCartInDb.Product.StockQuantity)
                productCartInDb.Quantity++;
            DBContext.SaveChanges();
        }

        public void DecrementQuantity(int cartId)
        {
            var productCartInDb = DBContext.ProductCarts.FirstOrDefault(pc => pc.CartId == cartId);
            if (productCartInDb.Quantity > 1)
                productCartInDb.Quantity--;
            DBContext.SaveChanges();
        }
        //public void RemoveFromCart(int cartId,int productId)
        //{
        //    var productCartInDb = DBContext.ProductCarts.FirstOrDefault(pc => pc.CartId == cartId);
        //    if (productCartInDb.Quantity > 1)
        //        productCartInDb.Quantity--;
        //    DBContext.SaveChanges();
        //}
    }
}
