using ECommerceMS.Data;
using ECommerceMS.Models;

namespace ECommerceMS.services.repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ECommerceDB _context;

        public AdminRepository(ECommerceDB context)
        {
            _context = context;
        }

        public int Create(Admin admin)
        {
            _context.Admins.Add(admin);
            int rows = _context.SaveChanges();
            return rows;
        }
    }
}
