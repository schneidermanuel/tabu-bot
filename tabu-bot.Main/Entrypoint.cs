using Autofac;
using tabu_bot.Configuration;
using tabu_bot.DataAccess;
using tabu_bot.DataAccess.SessionFactory;

namespace tabu_bot;

internal class Entrypoint
{
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<MainModule>();
        builder.RegisterModule<DataAccessModule>();
        var container = builder.Build();
        var system = container.Resolve<BotSystem>();
        var configService = container.Resolve<IConfigurationService>();
        var config = configService.ReadConfiguration();
        var databaseConfigurator = container.Resolve<IDatabaseConfigurator>();
        databaseConfigurator.Initialize(config.DatabaseConnectionString);
        _ = system.RunAsync(config);
        Console.WriteLine("Started Bot");
        Task.Delay(-1).GetAwaiter().GetResult();
    }
}