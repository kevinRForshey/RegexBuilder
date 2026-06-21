using System.Diagnostics;

namespace Regex.Core;

public sealed record Quantifier(int Min = 1, int? Max = 1)
{
    public static readonly Quantifier One = new(1, 1);

    public string Build() => (Min, Max) switch
    {
        (1, 1) => "",
        (0, 1) => "?",
        (0, null) => "*",
        (1, null) => "+",
        (var min, null) => $"{{{min},}}",
        (var min, int max) when min == max => $"{{{max}}}",
        (var min, int max) => $"{{{min},{max}}}",
    };
    
    public string Describe() => (Min, Max) switch
    {
        (1, 1)                           => "once",
        (0, 1)                           => "optionally",
        (0, null)                        => "zero or more times",
        (1, null)                        => "one or more times",
        (var min, null)                  => $"at least {min} times",
        (var min, int max) when min==max => $"exactly {min} times",
        (var min, int max)               => $"between {min} and {max} times",
    };
}