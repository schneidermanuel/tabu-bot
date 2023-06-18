namespace tabu_bot.DataAccess.Entity;

public class CardSetEntity
{
    public virtual long CardSetId { get; set; }
    public virtual string Name { get; set; }
    public virtual string OwnerId { get; set; }
    public virtual ISet<CardEntity> Cards { get; set; }
}