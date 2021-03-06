using ECommerceMS.Data;
using ECommerceMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceMS.services
{
    public class ProductRepository : IProductRepository
    {
        readonly ECommerceDB DBContext;

        public ProductRepository(ECommerceDB _DBContext)
        {
            DBContext = _DBContext;
        }

        public List<Product> GetAll()
        {
            return DBContext.Products.Include(p=> p.Category).ToList();
        }

        public Product GetById(int id)
        {
            return DBContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public int Create(Product prod)
        {
            DBContext.Products.Add(prod);
            int raw = DBContext.SaveChanges();
            return raw;
        }

        public int Update(int id, Product prod)
        {
            Product product = DBContext.Products.FirstOrDefault(p => p.Id == id);
            product.Name = prod.Name;
            product.Image = prod.Image;
            product.Description = prod.Description;
            product.Size = prod.Size;
            product.Color = prod.Color;
            product.Price = prod.Price;
            product.CategoryId = prod.CategoryId;
            product.StockQuantity = prod.StockQuantity;

            int raw = DBContext.SaveChanges();
            return raw;
        }

        public int Delete(int id)
        {
            Product product = DBContext.Products.FirstOrDefault(p => p.Id == id);
            DBContext.Products.Remove(product);
            int raw = DBContext.SaveChanges();
            return raw;
        }
        public List<Product> GetFavouriteProducts(string customerId)
        {
            var products = DBContext.FavouriteLists.Where(f => f.CustomerId == customerId).Select(f => f.Product).ToList();
            return products;
        }

        public int AddToFavouriteList(string customerId, int productId)
        {
            var productInDb = DBContext.Products.FirstOrDefault(p => p.Id == productId);
            var customerInDb = DBContext.Customers.FirstOrDefault(c => c.Id == customerId);
            var favListInDb = DBContext.FavouriteLists.Where(f => f.ProductId == productId && f.CustomerId == customerId).ToList();
            if (!(favListInDb.Count() > 0))
            {

                var favList = new FavouriteList() { CustomerId = customerId, ProductId = productId };

                productInDb.FavouriteList.Add(favList);
                customerInDb.FavouriteList.Add(favList);
            }

            return DBContext.SaveChanges();

        }

        public int RemoveFromFavouriteList(string customerId, int productId)
        {
            var productInDb = DBContext.Products.FirstOrDefault(p => p.Id == productId);
            var customerInDb = DBContext.Customers.FirstOrDefault(c => c.Id == customerId);
            var favListInDb = DBContext.FavouriteLists.FirstOrDefault(f => f.ProductId == productId && f.CustomerId == customerId);
            if (favListInDb !=null)
            {
                productInDb.FavouriteList.Remove(favListInDb);
                customerInDb.FavouriteList.Remove(favListInDb);
            }

            return DBContext.SaveChanges();

        }
        public int DecrementStockQuantity(int ProductId, int Quantity)
        {
            var Product = DBContext.Products.FirstOrDefault(x => x.Id == ProductId);
            Product.StockQuantity = Product.StockQuantity-Quantity;
            return DBContext.SaveChanges();
        }
    }
}
