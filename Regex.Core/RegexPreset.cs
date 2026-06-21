namespace Regex.Core;
public sealed record RegexPreset(string Name, string Pattern, string Description);

public interface IRegexPresetProvider
{
    IReadOnlyList<RegexPreset> GetPresets();
}

public sealed class RegexPresetProvider : IRegexPresetProvider
{
    // Pragmatic, NOT bulletproof. A fully RFC-5322-correct email regex is monstrous
    // and you shouldn't ship one. IPv4 here doesn't range-check 0–255 octets.
    public IReadOnlyList<RegexPreset> GetPresets() => new[]
    {
        new RegexPreset("Email",            @"[\w.+-]+@[\w-]+\.[\w.-]+",                 "a pragmatic email address"),
        new RegexPreset("URL (http/https)", @"https?://[^\s]+",                          "an http or https URL"),
        new RegexPreset("US phone",         @"\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}",     "a US phone number"),
        new RegexPreset("IPv4 address",     @"(?:\d{1,3}\.){3}\d{1,3}",                 "an IPv4 address (not range-validated)"),
        new RegexPreset("Date YYYY-MM-DD",  @"\d{4}-\d{2}-\d{2}",                       "an ISO-style date"),
        new RegexPreset("Hex colour",       @"#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6})",      "a hex colour code"),
    };
}

