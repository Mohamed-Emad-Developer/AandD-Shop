using ECommerceMS.Data;
using ECommerceMS.Models;

namespace ECommerceMS.services.repository
{
    public class ProductOrdersRepository : IProductOrdersRepository
    {
        ECommerceDB DBContext;
        public ProductOrdersRepository(ECommerceDB _DBContext)
        {
            DBContext = _DBContext;
        }
        public int Create(int OrderNumber, int ProductId ,int quantity)
        {
            var productOrder = new ProductOrders() { OrderNum = OrderNumber, ProductId = ProductId ,Quantity=quantity};
            DBContext.ProductOrders.Add(productOrder);
            return DBContext.SaveChanges();
        }
    }
}
