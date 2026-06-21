using System.Text.RegularExpressions;

namespace Regex.Core;

public sealed record RegexResult(string Pattern, RegexOptions Options, string Description);