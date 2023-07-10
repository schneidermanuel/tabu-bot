using Autofac;
using tabu_bot.Framework;

namespace tabu_bot.Commands;

internal class CommandsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TabuAdminCommands>().As<ICommandModule>();
        builder.RegisterType<TabuCommands>().As<ICommandModule>();
        base.Load(builder);
    }
}