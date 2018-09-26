namespace AnyCompany
{
    public interface IVatApplicator
    {
        void ApplyVat(Order o, Customer c);
    }
}