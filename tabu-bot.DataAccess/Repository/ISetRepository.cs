using tabu_bot.DataAccess.Data;

namespace tabu_bot.DataAccess.Repository;

public interface ISetRepository
{
    Task<long> CreateSetAsync(string name, ulong ownerId);
    Task<IEnumerable<CardSet>> RetrieveMySetsAsync(ulong userId);
    Task<bool> UnassignChannel(ulong channelId);
    Task AssignSetAsync(long id, ulong channelId);
    Task<CardSet> RetrieveSetByIdAsync(long id);
    Task<bool> CanCreateAsync(string name, ulong channelId);

    Task SaveCardAsync(string name, ulong channelId, string userUsername, string word1, string word2, string word3,
        string word4);
}