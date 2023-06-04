using Autofac;
using tabu_bot.Configuration;

namespace tabu_bot;

internal class MainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<ConfigurationModule>();
        builder.RegisterType<BotSystem>().AsSelf().SingleInstance();
        base.Load(builder);
    }
}