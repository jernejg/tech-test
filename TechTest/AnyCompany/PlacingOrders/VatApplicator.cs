using System;
using AnyCompany.Api;

namespace AnyCompany.PlacingOrders
{
    public class VatApplicator : IVatApplicator
    {
        public void ApplyVat(Order o, Customer c)
        {
            o.VAT = 0;

            if (string.Equals(c.Country, "UK", StringComparison.InvariantCultureIgnoreCase))
                o.VAT = 0.2d;
        }
    }
}