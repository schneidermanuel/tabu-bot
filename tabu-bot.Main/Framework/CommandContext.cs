using System.Reflection;

namespace tabu_bot.Framework;

internal class CommandContext
{
    public ICommandModule Instance { get; }
    public MethodInfo Method { get; }

    public CommandContext(ICommandModule instance, MethodInfo method)
    {
        Instance = instance;
        Method = method;
    }
}