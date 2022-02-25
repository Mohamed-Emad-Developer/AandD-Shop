namespace ECommerceMS.services.repository
{
    public interface IProductOrdersRepository
    {
        int Create(int OrderNumber, int ProductId, int quantity);
    }
}