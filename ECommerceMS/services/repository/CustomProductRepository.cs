using ECommerceMS.Data;

namespace ECommerceMS.services.repository
{
    public class CustomProductRepository
    {
        ECommerceDB context;
        public CustomProductRepository(ECommerceDB _context)
        {
            context = _context;
        }
        //public int Create(Category Ctg)
        //{
        //    context.Categories.Add(Ctg);
        //    int raw = context.SaveChanges();
        //    return raw;
        //}

    }
}
