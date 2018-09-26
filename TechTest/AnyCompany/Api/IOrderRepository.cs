namespace AnyCompany.Api
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}