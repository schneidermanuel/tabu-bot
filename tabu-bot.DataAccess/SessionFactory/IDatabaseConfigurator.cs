namespace tabu_bot.DataAccess.SessionFactory;

public interface IDatabaseConfigurator
{
    void Initialize(string connectionString);
}