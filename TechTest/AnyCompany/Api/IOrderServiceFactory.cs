namespace AnyCompany.Api
{
    public interface IOrderServiceFactory
    {
        IOrderService Create();
    }
}