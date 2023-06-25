namespace tabu_bot.DataAccess.Data;

public class CardSet
{
    public long SetId { get; }
    public string Name { get; }
    public ulong OwnerId { get; }

    public CardSet(long setId, string name, ulong ownerId)
    {
        SetId = setId;
        Name = name;
        OwnerId = ownerId;
    }
}