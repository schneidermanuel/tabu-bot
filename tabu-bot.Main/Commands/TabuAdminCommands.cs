using Discord.Interactions;
using Discord.WebSocket;
using tabu_bot.DataAccess.Repository;
using tabu_bot.Framework;

namespace tabu_bot.Commands;

internal class TabuAdminCommands : ICommandModule
{
    private readonly ISetRepository _repository;

    public TabuAdminCommands(ISetRepository repository)
    {
        _repository = repository;
    }

    [SlashCommand("create-tabu-set", "Creates a new Set of tabu cards")]
    [Parameter("name", "the name of the set to create", true, false)]
    public async Task CreateSetCommand(SocketSlashCommand slashCommand)
    {
        var name = (string)slashCommand.Data.Options.Single(option => option.Name == "name").Value;
        var ownerId = slashCommand.User.Id;
        var id = await _repository.CreateSetAsync(name, ownerId);
        await slashCommand.RespondAsync($"Created new Card-Set '{name}' with Id '{id}'", ephemeral: true);
    }

    [SlashCommand("list-tabu-sets", "Lists your tabu sets")]
    public async Task ShowMySetsAsync(SocketSlashCommand slashCommand)
    {
        var sets = await _repository.RetrieveMySetsAsync(slashCommand.User.Id);
        var text = $"Tabu sets of '{slashCommand.User.Username}'\n\n";
        text = sets.Aggregate(text, (current, set) => current + $"{set.SetId}) {set.Name}\n");
        await slashCommand.RespondAsync(text, ephemeral: true);
    }

    [SlashCommand("assign-tabu-set", "ASsigns a existing Set to this channel")]
    [Parameter("id", "the id of the set to assign", true, true)]
    public async Task AssignSetToChannelAsync(SocketSlashCommand slashCommand)
    {
        if (slashCommand.User is not SocketGuildUser guildUser)
        {
            await slashCommand.RespondAsync("This command can only be executed in a guild", ephemeral: true);
            return;
        }

        if (!guildUser.GuildPermissions.Administrator)
        {
            await slashCommand.RespondAsync("This command can only be executed by an administrator", ephemeral: true);
        }

        var channelId = slashCommand.Channel.Id;
        var id = (long)slashCommand.Data.Options.Single(option => option.Name == "id").Value;
        var set = await _repository.RetrieveSetByIdAsync(id);
        var couldUnassign = await _repository.UnassignChannel(channelId);
        await _repository.AssignSetAsync(id, channelId);
        await slashCommand.RespondAsync($"Assigned set '{set.Name}' to this channel");
        if (couldUnassign)
        {
            await slashCommand.FollowupAsync("The previously assigned set was removed", ephemeral: true);
        }
    }
}