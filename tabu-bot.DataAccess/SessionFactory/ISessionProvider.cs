using NHibernate;

namespace tabu_bot.DataAccess.SessionFactory;

internal interface ISessionProvider
{
    ISession CreateSession();
    void Init(ISessionFactory sessionFactory);
}