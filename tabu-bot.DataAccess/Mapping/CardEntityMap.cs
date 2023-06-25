using FluentNHibernate.Mapping;
using tabu_bot.DataAccess.Entity;

namespace tabu_bot.DataAccess.Mapping;

public class CardEntityMap : ClassMap<CardEntity>
{
    public CardEntityMap()
    {
        Table("card");
        Id(entity => entity.CardId, "cardId");
        Map(entity => entity.ContributerName, "contributerName");
        Map(entity => entity.Text, "text");
        Map(entity => entity.Keyword1, "keyword1");
        Map(entity => entity.Keyword2, "keyword2");
        Map(entity => entity.Keyword3, "keyword3");
        Map(entity => entity.Keyword4, "keyword4");
        References(entity => entity.Set, "cardSetId");
    }
}