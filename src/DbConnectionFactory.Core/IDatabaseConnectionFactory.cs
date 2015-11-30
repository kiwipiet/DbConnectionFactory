namespace DbConnectionFactory
{
    public interface IDatabaseConnectionFactory
    {
        IDatabaseConnection Create();
    }
}