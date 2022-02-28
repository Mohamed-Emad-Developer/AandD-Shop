using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.services
{
    public interface IProductRepository
    {
        int Create(Product prod);
        int Delete(int id);
        List<Product> GetAll();
        Product GetById(int id);
        int Update(int id, Product prod);
        List<Product> GetFavouriteProducts(string customerId);
        int AddToFavouriteList(string customerId, int productId);
        int RemoveFromFavouriteList(string customerId, int productId);
    }
}