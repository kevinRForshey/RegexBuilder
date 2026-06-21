namespace Regex.Core;

public interface IRegexRule
{
    /// <summary>
    /// Regex fragment that represents this rule. It should be a valid regex fragment that can be used in a larger regex pattern.
    /// </summary>
    /// <returns></returns>
    string Build();
    
    /// <summary>
    /// Plain-English description.
    /// Used by th explanation panel
    /// </summary>
    /// <returns></returns>
    string Describe();
}