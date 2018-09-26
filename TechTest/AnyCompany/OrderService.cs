namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderValidator _orderValidator;
        private readonly IVatApplicator _vatApplicator;

        public OrderService(IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IOrderValidator orderValidator,
            IVatApplicator vatApplicator)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderValidator = orderValidator;
            _vatApplicator = vatApplicator;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            var customer = _customerRepository.Load(customerId);

            if (!_orderValidator.IsValid(order))
                return false;

            _vatApplicator.ApplyVat(order, customer);

            _orderRepository.Save(order);

            return true;
        }
    }
}
