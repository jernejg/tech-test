using System;

namespace AnyCompany
{
    public class OrderValidator : IOrderValidator
    {
        public bool IsValid(Order o)
        {
            if (o.Amount < 0.01)
                return false;

            return true;
        }
    }
}