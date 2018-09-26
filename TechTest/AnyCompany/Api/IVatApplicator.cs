namespace AnyCompany.Api
{
    public interface IVatApplicator
    {
        void ApplyVat(Order o, Customer c);
    }
}