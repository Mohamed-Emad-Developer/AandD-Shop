using ECommerceMS.Data;
using ECommerceMS.Models;
using ECommerceMS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMS.services.repository
{
    public class CustomerRepository : ICustomerRepository
    {
        ECommerceDB DBContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomerRepository(ECommerceDB _DBContext, UserManager<ApplicationUser> userManager)
        {
            DBContext = _DBContext;
            _userManager = userManager;
        }

        public int Create(string Id, string address, string phone)
        {
            Customer customer = new Customer()
            {
                Id =  Id,
                Address = address,
                Phone = phone,
            };
            DBContext.Customers.Add(customer);
            int rows = DBContext.SaveChanges();
            return rows;
        }

        public Customer Get(string id)
        {
            return DBContext.Customers.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }
        public int UpdateNAP(string id , OrderDetailsViewModel newDetails) //update only address&Name&phone to Customer
        {
            var customer = DBContext.Customers.Include(x => x.User).FirstOrDefault(x => x.Id == id);
            
            customer.User.Name= newDetails.Name;
            customer.Address = newDetails.Adress;
            customer.Phone = newDetails.Phone;
            return DBContext.SaveChanges();
        }
        public int Edit(string id, string address, string phone)
        {
            var customerInDb = DBContext.Customers.FirstOrDefault(c => c.Id == id);
            customerInDb.Address = address;
            customerInDb.Phone = phone;
            
            return DBContext.SaveChanges();
        }
        
    }
}
