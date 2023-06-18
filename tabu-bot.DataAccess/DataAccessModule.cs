using Autofac;
using tabu_bot.DataAccess.SessionFactory;

namespace tabu_bot.DataAccess;

public class DataAccessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<SessionFactoryModule>();
        base.Load(builder);
    }
}