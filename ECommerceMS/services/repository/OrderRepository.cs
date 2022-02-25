using ECommerceMS.Data;
using ECommerceMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceMS.services.repository
{
    public class OrderRepository: IOrderRepository
    {
        readonly ECommerceDB DBContext;
        public OrderRepository(ECommerceDB _DBContext)
        {
            DBContext = _DBContext;        
        }
        public int Create(decimal Cost, string Customer_ID) //return orderNumber after create the order
        {
            DateTime now = DateTime.Now;
            Order NewOrder=new Order() {Cost = Cost , Date=now ,CustomerId=Customer_ID};
            DBContext.Orders.Add(NewOrder);
            DBContext.SaveChanges();
            DBContext.Entry(NewOrder).GetDatabaseValues();
            return NewOrder.OrderNum;
        }
        public List<Order> Get()
        {
            return (DBContext.Orders.Include(x => x.Customer).ThenInclude(x=>x.User)).Include(x => x.ProductOrders).ThenInclude(x => x.Product).ToList();
        }
        public Order Get(int id)
        {
            return (DBContext.Orders.Include(x => x.Customer).ThenInclude(x => x.User)).Include(x=>x.ProductOrders).ThenInclude(x=>x.Product).FirstOrDefault(x=>x.OrderNum==id);
        }
    }


}
