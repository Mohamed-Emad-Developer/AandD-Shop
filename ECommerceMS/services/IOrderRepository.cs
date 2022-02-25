using ECommerceMS.Models;
using System.Collections.Generic;

namespace ECommerceMS.services.repository
{
    public interface IOrderRepository
    {
        int Create(decimal Cost , string Customer_ID);
        List<Order> Get();
        Order Get(int id);
    }
}