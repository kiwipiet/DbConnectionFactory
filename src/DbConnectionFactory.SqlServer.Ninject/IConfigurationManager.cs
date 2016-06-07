using System.Collections.Specialized;

namespace DbConnectionFactory.SqlServer
{
    public interface IConfigurationManager
    {
        NameValueCollection AppSettings { get; }

        string GetConnectionString(string name);

        string GetProviderName(string name);

        T GetSection<T>(string sectionName) where T : class;
    }
}
