namespace DbConnectionFactory.Core
{
    public interface IDatabaseConnectionFactory
    {
        IDatabaseConnection Create();
    }
}