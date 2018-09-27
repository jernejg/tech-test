using System.Data.SqlClient;
using AnyCompany.Api;
using Dapper;

namespace AnyCompany.PlacingOrders
{
    internal class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            // This is the shortest sintax possible for storing a new Order as long as:
            //
            // - The order of the values is in same order as the columns in the table.
            // - We are adding values for all the columns in the table.
            // - Properties on the Order object have the same names as the table columns.
            var insertQuery = "INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)";
            using (var sqlConnection = new SqlConnection())
            {
                var result = sqlConnection.Execute(insertQuery, order);
            }
        }
    }
}
