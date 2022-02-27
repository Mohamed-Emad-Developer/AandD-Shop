using ECommerceMS.Models;
using ECommerceMS.ViewModel;

namespace ECommerceMS.services.repository
{
    public interface ICustomerRepository
    {
        Customer Get(string id);
        int UpdateNAP(string id, OrderDetailsViewModel newDetails);
        int Create(Customer customer);
    }
}