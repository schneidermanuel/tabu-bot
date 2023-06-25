using FluentNHibernate.Mapping;
using tabu_bot.DataAccess.Entity;

namespace tabu_bot.DataAccess.Mapping;

public class ChannelSetEntityMap : ClassMap<ChannelSetEntity>
{
    public ChannelSetEntityMap()
    {
        Table("channelSet");
        Id(entity => entity.ChannelSetId, "channelSetId");
        Map(entity => entity.ChannelId, "channelId");
        References(entity => entity.CardSet, "cardsetId");
    }
}