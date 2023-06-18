using NHibernate;

namespace tabu_bot.DataAccess.SessionFactory;

internal class SessionProvider : ISessionProvider
{
    private ISessionFactory _factory;

    public ISession CreateSession()
    {
        if (_factory == null)
        {
            throw new InvalidOperationException("The session has not been Initialized. Call Init first");
        }

        return _factory.OpenSession();
    }

    public void Init(ISessionFactory sessionFactory)
    {
        _factory = sessionFactory;
    }
}