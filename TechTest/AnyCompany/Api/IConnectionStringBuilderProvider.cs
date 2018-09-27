using System.Data.SqlClient;

namespace AnyCompany.Api
{
    public interface IConnectionStringBuilderProvider
    {
        SqlConnectionStringBuilder Get();
    }
}