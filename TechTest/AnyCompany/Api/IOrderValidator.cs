namespace AnyCompany.Api
{
    public interface IOrderValidator
    {
        bool IsValid(Order o);
    }
}