using AnyCompany.PlacingOrders;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        public static Customer Load(int customerId)
        {
            var custRepImpl = new CustomerRepositoryImplementation();
            return custRepImpl.Load(customerId);
        }
    }
}
