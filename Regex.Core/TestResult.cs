namespace Regex.Core;

public sealed record TestResult(
    bool IsValidPattern, bool IsMatch, IReadOnlyList<string> Matches, string? Error);
    
    