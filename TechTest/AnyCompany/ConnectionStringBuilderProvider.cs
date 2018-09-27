using System.Configuration;
using System.Data.SqlClient;
using AnyCompany.Api;

namespace AnyCompany
{
    public class ConnectionStringBuilderProvider : IConnectionStringBuilderProvider
    {
        public SqlConnectionStringBuilder Get()
        {
            var cs = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            return new SqlConnectionStringBuilder(cs);
        }
    }
}