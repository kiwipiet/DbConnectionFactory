using System.Linq;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Configuration;

namespace DbConnectionFactory.SqlServer
{
    public class DbConnectionFactoryModule : Ninject.Modules.NinjectModule
    {
        private readonly string _connectionString;
        public const string DefaultConnectionStringName = "DefaultConnection";
        private const string ConnectionStringName = "ConnectionStringName";

        public DbConnectionFactoryModule() 
            : this(GetConfigConnectionString())
        {
        }
        /// <summary>
        /// Use this only if you cannot use the default constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public DbConnectionFactoryModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        private static string GetConfigConnectionString()
        {
            var configurationManager = new ConfigurationManagerWrapper();
            var connectionStringName = GetConnectionStringName(configurationManager);
            return configurationManager.GetConnectionString(connectionStringName);
        }

        private static string GetConnectionStringName(IConfigurationManager configurationManager)
        {
            if (configurationManager.AppSettings.AllKeys.Contains(ConnectionStringName))
            {
                return configurationManager.AppSettings[ConnectionStringName];
            }
            return DefaultConnectionStringName;
        }

        public override void Load()
        {
            Bind<IDatabaseConnectionFactory>()
                .ToMethod(ctx => new DatabaseConnectionFactory(_connectionString));
        }
        private sealed class ConfigurationManagerWrapper : IConfigurationManager
        {
            public NameValueCollection AppSettings
            {
                [DebuggerStepThrough]
                get
                {
                    return ConfigurationManager.AppSettings;
                }
            }

            [DebuggerStepThrough]
            public string GetConnectionString(string name)
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }

            [DebuggerStepThrough]
            public string GetProviderName(string name)
            {
                return ConfigurationManager.ConnectionStrings[name].ProviderName;
            }

            public T GetSection<T>(string sectionName) where T : class
            {
                return ConfigurationManager.GetSection(sectionName) as T;
            }
        }

    }
}
