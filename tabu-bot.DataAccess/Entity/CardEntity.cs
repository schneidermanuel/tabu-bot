namespace tabu_bot.DataAccess.Entity;

public class CardEntity
{
    public virtual long CardId { get; set; }
    public virtual string ContributerName { get; set; }
    public virtual string Text { get; set; }
    public virtual string Keyword1 { get; set; }
    public virtual string Keyword2 { get; set; }
    public virtual string Keyword3 { get; set; }
    public virtual string Keyword4 { get; set; }
    public virtual CardSetEntity Set { get; set; }
}