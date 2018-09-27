using System.Configuration;
using System.Data.SqlClient;
using AnyCompany.Api;

namespace AnyCompany
{
    public class ConfigFileConnectionStringBuilderProvider : IConnectionStringBuilderProvider
    {
        private readonly string _connectionStringName;

        public ConfigFileConnectionStringBuilderProvider(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }
        public SqlConnectionStringBuilder Get()
        {
            var cs = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;

            return new SqlConnectionStringBuilder(cs);
        }
    }
}