using Autofac;
using tabu_bot.Commands;
using tabu_bot.Configuration;

namespace tabu_bot;

internal class MainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<ConfigurationModule>();
        builder.RegisterModule<CommandsModule>();
        builder.RegisterType<BotSystem>().AsSelf().SingleInstance();
        base.Load(builder);
    }
}