using ECommerceMS.Data;
using ECommerceMS.Models;
using ECommerceMS.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerceMS.services.repository
{
    public class CustomerRepository : ICustomerRepository
    {
        ECommerceDB DBContext;
        public CustomerRepository(ECommerceDB _DBContext)
        {
            DBContext = _DBContext;
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
    }
}
