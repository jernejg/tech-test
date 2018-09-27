namespace AnyCompany.Api
{
    public interface IOrderService
    {
        bool PlaceOrder(Order order, int customerId);
    }
}