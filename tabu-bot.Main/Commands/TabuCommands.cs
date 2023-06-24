using Discord.Interactions;
using Discord.WebSocket;
using tabu_bot.Framework;

namespace tabu_bot.Commands;

internal class TabuCommands : ICommandModule
{
    [SlashCommand("create-tabu-set", "Creates a new Set of tabu cards")]
    [Parameter("name", "the name of the set to create", true)]
    public async Task CreateSetCommand(SocketSlashCommand slashCommand)
    {
    }
}