using System.Data.SqlClient;
using AnyCompany.Api;

namespace AnyCompany.PlacingOrders
{
    internal class OrderRepository : IOrderRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Orders;User Id=admin;Password=password;";

        public void Save(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT, @CustomerId)", connection);

            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);
            command.Parameters.AddWithValue("@CustomerId", order.Customer.Id);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
