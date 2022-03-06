using ECommerceMS.Data;
using ECommerceMS.Models;
using ECommerceMS.ViewModel;
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
        public int Create(OrderDetailsViewModel newOrder, string Customer_ID) //return orderNumber after create the order
        {
            DateTime now = DateTime.Now;
            Order NewOrder=new Order() {Cost = newOrder.TotalCost , Date=now ,CustomerId=Customer_ID ,CustomerAddress=newOrder.Adress,CustomerName=newOrder.Name,CustomerPhone=newOrder.Phone};
            DBContext.Orders.Add(NewOrder);
            DBContext.SaveChanges();
            DBContext.Entry(NewOrder).GetDatabaseValues();
            return NewOrder.OrderNum;
        }
        public int CreateCustomProductOrder(CustomProductOrderViewModel newOrder)
        {
            DateTime now = DateTime.Now;
            Order NewOrder = new Order() { Cost = newOrder.Cost, Date = now, CustomerId = newOrder.CustomerID, CustomerAddress = newOrder.Adress, CustomerName = newOrder.Name, CustomerPhone = newOrder.Phone };
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
        public List<Order> GetByCusID(string id)
        {
            return DBContext.Orders.Include(x=>x.ProductOrders).ThenInclude(x=>x.Product).Where(x=>x.CustomerId==id).ToList();
        }
    }


}
