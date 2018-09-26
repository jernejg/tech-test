namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderValidator _orderValidator;

        public OrderService(IOrderRepository orderRepository, 
            ICustomerRepository customerRepository, 
            IOrderValidator orderValidator)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderValidator = orderValidator;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            var customer = _customerRepository.Load(customerId);

            if (!_orderValidator.IsValid(order))
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            _orderRepository.Save(order);

            return true;
        }
    }
}
