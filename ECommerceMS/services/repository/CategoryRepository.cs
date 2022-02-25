using ECommerceMS.Data;
using ECommerceMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceMS.services.repository
{
    public class CategoryRepository : ICategoryRepository
    {

        ECommerceDB context;
        public CategoryRepository(ECommerceDB _context)
        {
            context = _context;
        }
        public List<Category> getAll(string searchString)
        {
            return context.Categories.Where(c => c.Title!.Contains(searchString) || searchString == null).ToList();
        }

        public Category getById(int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public Category getByName(string title)
        {
            return context.Categories.FirstOrDefault(c => c.Title == title);
        }

        public int create(Category Ctg)
        {
            context.Categories.Add(Ctg);
            int raw = context.SaveChanges();
            return raw;
        }

        public int update(int id, Category Ctg)
        {
            Category ctg = context.Categories.FirstOrDefault(c => c.Id == id);
            ctg.Title = Ctg.Title;
            ctg.Image = Ctg.Image;
            int raw = context.SaveChanges();
            return raw;
        }
        public int delete(int id)
        {
            Category ctg = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Categories.Remove(ctg);
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
