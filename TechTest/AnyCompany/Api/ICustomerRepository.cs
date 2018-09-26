namespace AnyCompany.Api
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
    }
}