namespace tabu_bot.Framework;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
internal class ParameterAttribute : Attribute
{
    public string ParameterName { get; }
    public string ParameterDescription { get; }
    public bool Required { get; }
    public bool IsInt { get; }

    public ParameterAttribute(string parameterName, string parameterDescription, bool required, bool isInt)
    {
        ParameterName = parameterName;
        ParameterDescription = parameterDescription;
        Required = required;
        IsInt = isInt;
    }
}