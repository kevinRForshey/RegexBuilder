using System.Text.RegularExpressions;
using System.Text;

namespace Regex.Core;

public interface IRegexComposer
{
    RegexResult Compose(IReadOnlyList<IRegexRule> rules, RegexBuildOptions options);
}

public sealed class RegexComposer : IRegexComposer
{
    public RegexResult Compose(IReadOnlyList<IRegexRule> rules, RegexBuildOptions options)
    {
        var body = string.Concat(rules.Select(r => r.Build()));

        var sb = new StringBuilder();
        if (options.AnchorStart) sb.Append('^');
        sb.Append(body);
        if (options.AnchorEnd) sb.Append('$');
        var pattern = sb.ToString();

        var regexOptions = RegexOptions.None;
        if (options.IgnoreCase) regexOptions |= RegexOptions.IgnoreCase;
        if (options.Multiline)  regexOptions |= RegexOptions.Multiline;

        return new RegexResult(pattern, regexOptions, BuildDescription(rules, options));
    }

    private static string BuildDescription(IReadOnlyList<IRegexRule> rules, RegexBuildOptions o)
    {
        var parts = rules.Select(r => r.Describe())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();

        if (parts.Count == 0) return "Nothing to match yet — add a condition.";

        var sb = new StringBuilder("Matches ");
        if (o.AnchorStart) sb.Append("text that begins with ");
        sb.Append(string.Join(", then ", parts));
        if (o.AnchorEnd) sb.Append(", and then the text ends");
        sb.Append('.');
        if (o.IgnoreCase) sb.Append(" Case is ignored.");
        return sb.ToString();
    }
}