using Discord;
using Discord.WebSocket;

namespace tabu_bot;

internal class BotSystem
{
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
    }
}