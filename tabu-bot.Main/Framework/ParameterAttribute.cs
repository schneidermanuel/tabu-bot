namespace tabu_bot.Framework;

internal class ParameterAttribute : Attribute
{
    public string ParameterName { get; }
    public string ParameterDescription { get; }
    public bool Required { get; }

    public ParameterAttribute(string parameterName, string parameterDescription, bool required)
    {
        ParameterName = parameterName;
        ParameterDescription = parameterDescription;
        Required = required;
    }
}