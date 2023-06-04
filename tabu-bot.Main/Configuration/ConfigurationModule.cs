using Autofac;

namespace tabu_bot.Configuration;

internal class ConfigurationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ConfigurationService>().As<IConfigurationService>();

        base.Load(builder);
    }
}