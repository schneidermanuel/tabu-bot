using Autofac;
using tabu_bot.Configuration;

namespace tabu_bot;

internal class Entrypoint
{
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<MainModule>();
        var container = builder.Build();
        var system = container.Resolve<BotSystem>();
        var configService = container.Resolve<IConfigurationService>();
        var config = configService.ReadConfiguration();
        Console.WriteLine("Started Bot");
    }
}