using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.services.repository
{
    public interface ICategoryRepository
    {
        int Create(Category Ctg);
        int Delete(int id);
        List<Category> GetAll();
        List<Category> GetAllSearch(string searchString);
        Category GetById(int id);
        Category GetByName(string title);
        int Update(int id, Category Ctg);
    }
}