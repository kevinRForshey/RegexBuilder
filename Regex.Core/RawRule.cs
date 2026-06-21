namespace Regex.Core;

public sealed class RawRule(string pattern, string description) : IRegexRule
{
    public string Build() => pattern;

    public string Describe() =>
        string.IsNullOrEmpty(pattern) ? "No description provided" : description;
}