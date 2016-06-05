namespace DbConnectionFactory.SqlServer
{
    public class DbConnectionFactoryModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IDatabaseConnectionFactory>().To<DatabaseConnectionFactory>();
        }
    }
}
