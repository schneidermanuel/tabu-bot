using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace tabu_bot.DataAccess.SessionFactory;

internal class DatabaseConfigurator : IDatabaseConfigurator
{
    private readonly ISessionProvider _sessionProvider;

    public DatabaseConfigurator(ISessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    public void Initialize(string connnectionString)
    {
        var sessionFactory = Fluently
            .Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(connnectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DataAccessModule>())
            .BuildSessionFactory();

        _sessionProvider.Init(sessionFactory);
    }
}