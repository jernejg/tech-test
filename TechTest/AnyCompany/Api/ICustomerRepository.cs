using System.Collections.Generic;

namespace AnyCompany.Api
{
    public interface ICustomerRepository
    {
        Customer Load(int customerId);
        IEnumerable<Customer> Load();
    }
}