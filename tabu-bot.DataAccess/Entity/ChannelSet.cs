namespace tabu_bot.DataAccess.Entity;

internal class ChannelSet
{
    public virtual long ChannelSetId { get; set; }
    public virtual string ChannelId { get; set; }
    public virtual CardSetEntity CardSet { get; set; }
}