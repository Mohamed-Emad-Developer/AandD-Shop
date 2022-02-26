using ECommerceMS.Models;

namespace ECommerceMS.services
{
    public interface ICartRepository
    {
        Cart Get(int id);
    }
}
