using ECommerceMS.Data;
using ECommerceMS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMS.services.repository
{
    public class CartRepository: ICartRepository
    {
        readonly ECommerceDB DBContext;
        public CartRepository(ECommerceDB _DBContext)
        {
            DBContext = _DBContext;
        }
        public Cart Get(int id)
        {
            return DBContext.Carts.Include(x=>x.ProductCarts).ThenInclude(x=>x.Product).FirstOrDefault(x=>x.Id==id);
        }
    }
}
