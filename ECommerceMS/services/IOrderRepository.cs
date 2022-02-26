﻿using ECommerceMS.Models;
using ECommerceMS.ViewModel;
using System.Collections.Generic;

namespace ECommerceMS.services.repository
{
    public interface IOrderRepository
    {
        int Create(OrderDetailsViewModel newOrder, string Customer_ID);
        List<Order> Get();
        Order Get(int id);
    }
}