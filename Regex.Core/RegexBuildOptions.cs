namespace Regex.Core;

public sealed class RegexBuildOptions
{
    public bool AnchorStart { get; set; }
    public bool AnchorEnd { get; set; }
    public bool IgnoreCase { get; set; }
    public bool Multiline { get; set; }
}