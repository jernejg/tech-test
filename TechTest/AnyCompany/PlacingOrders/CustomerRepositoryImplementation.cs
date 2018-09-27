using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Api;
using Dapper;

namespace AnyCompany.PlacingOrders
{
    public class CustomerRepositoryImplementation : ICustomerRepository
    {
        private readonly IConnectionStringBuilderProvider _csProvider;

        public CustomerRepositoryImplementation(IConnectionStringBuilderProvider csProvider)
        {
            _csProvider = csProvider;
        }

        public Customer Load(int customerId)
        {
            var selectQuery = @"
                                    select * from Customers where Id = @customerId
                                    select * from Orders where CustomerId = @customerId
                              ";

            List<Order> orders;
            Customer customer;

            using (var sqlConnection = new SqlConnection(_csProvider.Get().ConnectionString))
            {
                using (var multi = sqlConnection.QueryMultiple(selectQuery, new { customerId }))
                {
                    customer = multi.ReadSingle<Customer>();
                    orders = multi.Read<Order>().ToList();
                }
            }

            foreach (var order in orders)
            {
                order.Customer = customer;
                order.CustomerId = customer.Id;
            }

            customer.Orders = orders;

            return customer;
        }

        public IEnumerable<Customer> Load()
        {
            var selectQuery = @"
                                    select * from Customers
                                    select * from Orders
                              ";

            Dictionary<int, Customer> customers;
            List<Order> orders;

            using (var sqlConnection = new SqlConnection(_csProvider.Get().ConnectionString))
            {
                using (var multi = sqlConnection.QueryMultiple(selectQuery))
                {
                    customers = multi.Read<Customer>().ToDictionary(x => x.Id, x => x);
                    orders = multi.Read<Order>().ToList();
                }
            }

            var ordersGroupedByCustomer = orders.GroupBy(x => x.CustomerId);

            foreach (var orderGroup in ordersGroupedByCustomer)
            {
                var customer = customers[orderGroup.Key];

                foreach (var order in orderGroup)
                {
                    order.Customer = customer;
                    order.CustomerId = customer.Id;
                }

                customer.Orders = orderGroup.Select(x=>x).ToList();
            }
                

            return customers.Values.ToList();
        }
    }
}