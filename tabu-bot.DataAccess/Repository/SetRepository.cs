using NHibernate.Linq;
using tabu_bot.DataAccess.Data;
using tabu_bot.DataAccess.Entity;
using tabu_bot.DataAccess.SessionFactory;

namespace tabu_bot.DataAccess.Repository;

internal class SetRepository : ISetRepository
{
    private readonly ISessionProvider _sessionProvider;

    public SetRepository(ISessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    public async Task<long> CreateSetAsync(string name, ulong ownerId)
    {
        using (var session = _sessionProvider.CreateSession())
        {
            var entity = new CardSetEntity
            {
                Name = name,
                OwnerId = ownerId.ToString()
            };
            await session.SaveOrUpdateAsync(entity);
            await session.FlushAsync();
            return entity.CardSetId;
        }
    }

    public async Task<IEnumerable<CardSet>> RetrieveMySetsAsync(ulong userId)
    {
        using (var session = _sessionProvider.CreateSession())
        {
            var entities = await session.Query<CardSetEntity>()
                .Where(entity => entity.OwnerId == userId.ToString())
                .ToListAsync();
            var datas = entities
                .Select(entity => new CardSet(entity.CardSetId, entity.Name, ulong.Parse(entity.OwnerId)))
                .ToArray();
            return datas;
        }
    }

    public async Task<bool> UnassignChannel(ulong channelId)
    {
        using (var session = _sessionProvider.CreateSession())
        {
            var existingEntity = await session.Query<ChannelSetEntity>()
                .Where(entity => entity.ChannelId == channelId.ToString())
                .SingleOrDefaultAsync();
            if (existingEntity == null)
            {
                await session.FlushAsync();
                return false;
            }

            await session.DeleteAsync(existingEntity);
            await session.FlushAsync();
            return true;
        }
    }

    public async Task AssignSetAsync(long id, ulong channelId)
    {
        using (var session = _sessionProvider.CreateSession())
        {
            var set = await session.LoadAsync<CardSetEntity>(id);
            var entity = new ChannelSetEntity
            {
                CardSet = set,
                ChannelId = channelId.ToString()
            };
            await session.SaveOrUpdateAsync(entity);
            await session.FlushAsync();
        }
    }

    public async Task<CardSet> RetrieveSetByIdAsync(long id)
    {
        using (var session = _sessionProvider.CreateSession())
        {
            var set = await session.GetAsync<CardSetEntity>(id);
            if (set == null)
            {
                await session.FlushAsync();
                return null;
            }

            var data = new CardSet(set.CardSetId, set.Name, ulong.Parse(set.OwnerId));
            await session.FlushAsync();
            return data;
        }
    }
}