using AnyCompany.Api;
using AnyCompany.PlacingOrders;

namespace AnyCompany
{
    public class OrderServiceFactory : IOrderServiceFactory
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConnectionStringBuilderProvider _connectionStringBuilderProvider;
        private readonly IOrderValidator _orderValidator;
        private readonly IVatApplicator _vatApplicator;

        public OrderServiceFactory(ICustomerRepository customerRepository,
            IConnectionStringBuilderProvider connectionStringBuilderProvider,
            IOrderValidator orderValidator,
            IVatApplicator vatApplicator)
        {
            _customerRepository = customerRepository;
            _connectionStringBuilderProvider = connectionStringBuilderProvider;
            _orderValidator = orderValidator;
            _vatApplicator = vatApplicator;
        }


        public IOrderService Create()
        {
            var orderRepository = new OrderRepository(_connectionStringBuilderProvider);
            return new OrderService(orderRepository, _customerRepository, _orderValidator, _vatApplicator);
        }
    }
}