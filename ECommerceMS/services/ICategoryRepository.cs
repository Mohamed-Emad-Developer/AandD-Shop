using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.services.repository
{
    public interface ICategoryRepository
    {
        int create(Category Ctg);
        int delete(int id);
        List<Category> getAll();
        Category getById(int id);
        Category getByName(string title);
        int update(int id, Category Ctg);
    }
}