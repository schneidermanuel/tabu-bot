namespace tabu_bot.Configuration;

[Serializable]
public class Configuration
{
    public string DatabaseConnectionString { get; set; }
    public string BotToken { get; set; }
}