using Discord.Interactions;
using Discord.WebSocket;
using tabu_bot.DataAccess.Repository;
using tabu_bot.Framework;

namespace tabu_bot.Commands;

internal class TabuCommands : ICommandModule
{
    private readonly ISetRepository _repository;

    public TabuCommands(ISetRepository repository)
    {
        _repository = repository;
    }

    [SlashCommand("create-tabu-card", "creates a new tabu card")]
    [Parameter("name", "name of the card", true, false)]
    [Parameter("word1", "forbidden word 1", true, false)]
    [Parameter("word2", "forbidden word 2", true, false)]
    [Parameter("word3", "forbidden word 3", true, false)]
    [Parameter("word4", "forbidden word 4", true, false)]
    public async Task CreateCardAsync(SocketSlashCommand slashCommand)
    {
        var name = (string)slashCommand.Data.Options.Single(x => x.Name == "name").Value;
        var channelId = slashCommand.Channel.Id;
        var canCreate = await _repository.CanCreateAsync(name, channelId);
        if (!canCreate)
        {
            await slashCommand.RespondAsync(
                "Unable to create card. It might already exist or no set is assigned to this channel", ephemeral: true);
            return;
        }

        var word1 = (string)slashCommand.Data.Options.Single(x => x.Name == "word1").Value;
        var word2 = (string)slashCommand.Data.Options.Single(x => x.Name == "word2").Value;
        var word3 = (string)slashCommand.Data.Options.Single(x => x.Name == "word3").Value;
        var word4 = (string)slashCommand.Data.Options.Single(x => x.Name == "word4").Value;
        await _repository.SaveCardAsync(name, channelId, slashCommand.User.Username, word1, word2, word3, word4);
        await slashCommand.RespondAsync($"Created card '{name}'. Words: '{word1}', '{word2}', '{word3}', '{word4}'");
    }
}