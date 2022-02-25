using ECommerceMS.Data;
using ECommerceMS.Models;
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
            return DBContext.Products.ToList();
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
        public List<FavouriteList> GetFavouriteProducts(string customerId)
        {
            var products = DBContext.FavouriteLists.Where(f => f.CustomerId ==  customerId).ToList();
            return products;
        }
    }
}
