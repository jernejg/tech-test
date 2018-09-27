using System;
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
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customers WHERE CustomerId = " + customerId,
                connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }

            connection.Close();

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

            using (var sqlConnection = new SqlConnection())
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