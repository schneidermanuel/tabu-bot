namespace tabu_bot.DataAccess.Entity;

public class ChannelSetEntity
{
    public virtual long ChannelSetId { get; set; }
    public virtual string ChannelId { get; set; }
    public virtual CardSetEntity CardSet { get; set; }
}