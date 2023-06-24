using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using tabu_bot.Framework;

namespace tabu_bot;

internal class BotSystem
{
    private readonly IReadOnlyList<ICommandModule> _commandModules;
    private readonly IDictionary<string, CommandContext> _slashCommands;

    public BotSystem(IEnumerable<ICommandModule> commandModules)
    {
        _commandModules = commandModules.ToArray();
        _slashCommands = new Dictionary<string, CommandContext>();
    }

    private DiscordSocketClient _client = new();

    public async Task RunAsync(Configuration.Configuration configuration)
    {
        _client.Ready += ClientReady;
        _client.Log += ClientLog;
        await _client.LoginAsync(TokenType.Bot, configuration.BotToken);
        await _client.StartAsync();
        Console.WriteLine("Started System");
    }

    private async Task ClientLog(LogMessage arg)
    {
        Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {arg.Message}");
        await Task.CompletedTask;
    }

    private async Task ClientReady()
    {
        _client.Ready -= ClientReady;
        await _client.SetGameAsync("Tabu für Luci <3");
        await _client.SetStatusAsync(UserStatus.Online);
        await RegisterSlashCommands();
        _client.SlashCommandExecuted += SlashCommandExecuted;
    }

    private async Task SlashCommandExecuted(SocketSlashCommand arg)
    {
        var name = arg.CommandName;
        var context = _slashCommands[name];
        var task = (Task)context.Method.Invoke(context.Instance, new object[] { arg });
        Debug.Assert(task != null, nameof(task) + " != null");
        await task;
    }

    private async Task RegisterSlashCommands()
    {
        foreach (var commandModule in _commandModules)
        {
            var type = commandModule.GetType();
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            await BuildSlashCommandsAsync(methods, commandModule);
        }
    }

    private async Task BuildSlashCommandsAsync(IReadOnlyCollection<MethodInfo> methodInfos, ICommandModule instance)
    {
        foreach (var info in methodInfos)
        {
            var slashCommandAttribute = info.GetCustomAttribute<SlashCommandAttribute>();
            if (slashCommandAttribute == null)
            {
                continue;
            }

            var name = slashCommandAttribute.Name;
            var description = slashCommandAttribute.Description;
            var parameters = RetrieveParameters(info);
            var builder = new SlashCommandBuilder()
                .WithName(name)
                .WithDescription(description);
            foreach (var parameter in parameters)
            {
                builder.AddOption(new SlashCommandOptionBuilder()
                    .WithName(parameter.ParameterName)
                    .WithDescription(parameter.ParameterDescription)
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(parameter.Required));
            }

            await _client.CreateGlobalApplicationCommandAsync(builder.Build());
            _slashCommands.Add(name, new CommandContext(instance, info));
        }
    }

    private IReadOnlyList<ParameterAttribute> RetrieveParameters(MethodInfo info)
    {
        var parameterAttributes = info.GetCustomAttributes<ParameterAttribute>();
        return parameterAttributes.ToArray();
    }
}