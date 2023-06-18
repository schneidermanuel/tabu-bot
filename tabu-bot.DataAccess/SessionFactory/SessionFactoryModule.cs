using Autofac;

namespace tabu_bot.DataAccess.SessionFactory;

internal class SessionFactoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SessionProvider>().As<ISessionProvider>().SingleInstance();
        builder.RegisterType<DatabaseConfigurator>().As<IDatabaseConfigurator>().SingleInstance();

        base.Load(builder);
    }
}