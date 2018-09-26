namespace AnyCompany
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
    }
}