using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AnyCompany.Api;
using Dapper;

namespace AnyCompany.PlacingOrders
{
    public class CustomerRepositoryImplementation : ICustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public Customer Load(int customerId)
        {
            var selectQuery = @"
                                    select * from Customers where Id = @customerId
                                    select * from Orders where CustomerId = @customerId
                              ";

            List<Order> orders;
            Customer customer;

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using (var multi = sqlConnection.QueryMultiple(selectQuery, new { customerId }))
                {
                    customer = multi.ReadSingle<Customer>();
                    orders = multi.Read<Order>().ToList();
                }
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

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using (var multi = sqlConnection.QueryMultiple(selectQuery))
                {
                    customers = multi.Read<Customer>().ToDictionary(x => x.Id, x => x);
                    orders = multi.Read<Order>().ToList();
                }
            }

            var ordersGroupedByCustomer = orders.GroupBy(x => x.CustomerId);

            foreach (var orderGroup in ordersGroupedByCustomer)
                customers[orderGroup.Key].Orders = orderGroup;

            return customers.Values;
        }
    }
}