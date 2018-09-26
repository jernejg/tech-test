namespace AnyCompany
{
    public class VatApplicator : IVatApplicator
    {
        public void ApplyVat(Order o, Customer c)
        {
            o.VAT = 0;

            if (c.Country == "UK")
                o.VAT = 0.2d;
        }
    }
}