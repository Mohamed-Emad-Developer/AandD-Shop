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
    }
}