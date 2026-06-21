using System.Text.RegularExpressions;

namespace Regex.Core;

public sealed class LiteralRule (string text, Quantifier? quantifier = null) : IRegexRule
{
    private readonly Quantifier _q = quantifier ?? Quantifier.One;

    public string Build()
    {
        if (string.IsNullOrEmpty(text)) return "";
        var escaped = System.Text.RegularExpressions.Regex.Escape(text);
        var q = _q.Build();
        return (text.Length > 1 && q.Length > 0) ? $"(?:{escaped}){q}" : $"{escaped}{q}";
    }

    public string Describe() =>
        string.IsNullOrEmpty(text) ? "" : $"the text \u201c{text}\u201d {_q.Describe()}";
}
