using System.Reflection;
using System.Xml.Serialization;

namespace tabu_bot.Configuration;

internal class ConfigurationService : IConfigurationService
{
    public Configuration ReadConfiguration()
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
            "config.xml");
        var serializer = new XmlSerializer(typeof(Configuration));
        var fileStream = new FileStream(path, FileMode.Open);
        var config = serializer.Deserialize(fileStream) as Configuration;
        return config;
    }
}