using FluentNHibernate.Mapping;
using tabu_bot.DataAccess.Entity;

namespace tabu_bot.DataAccess.Mapping;

public class CardSetEntityMap : ClassMap<CardSetEntity>
{
    public CardSetEntityMap()
    {
        Table("cardSet");
        Id(entity => entity.CardSetId, "cardsetId");
        Map(entity => entity.OwnerId, "cardsetOwner");
        Map(entity => entity.Name, "cardsetName");
        HasMany(entity => entity.Cards).Inverse();
    }
}