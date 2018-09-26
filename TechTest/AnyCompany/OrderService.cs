namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public bool PlaceOrder(Order order, int customerId)
        {
            var customer = _customerRepository.Load(customerId);

            if (order.Amount == 0)
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
