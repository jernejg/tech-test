using AnyCompany.PlacingOrders;

namespace AnyCompany.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringBuilderProvider = new ConfigFileConnectionStringBuilderProvider("MyConnectionString");
            var customerRepository = new CustomerRepositoryImplementation(connectionStringBuilderProvider);
            var orderValidator = new OrderValidator();
            var vatApplicator = new VatApplicator();

            var orderServiceFactory = new OrderServiceFactory(customerRepository,
                connectionStringBuilderProvider, orderValidator, vatApplicator);

            var orderService = orderServiceFactory.Create();

            //Load all customers and their linked orders
            var customers = customerRepository.Load(3);

            //Place an order, linked to a customer
            var success = orderService.PlaceOrder(new Order()
            {
                OrderId = 1,
                Amount = 1,
            }, 1);
        }
    }
}
