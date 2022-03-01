using ECommerceMS.Models;

namespace ECommerceMS.services
{
    public interface ICartRepository
    {
        Cart Get(int id);
        int Create(string customerId);
        int AddPrpductToCart(int prodcutId, string customerId);
        Cart GetByUserId(string customerId);
        void IncrementQuantity(int cartId);
        void DecrementQuantity(int cartId);
    }
}
