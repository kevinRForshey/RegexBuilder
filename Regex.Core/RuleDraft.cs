namespace Regex.Core;

public enum RuleKind { Literal, CharacterClass, RawPattern }

public sealed class RuleDraft
{
    public RuleKind Kind { get; set; } = RuleKind.CharacterClass;
    public string Text { get; set; } = "";
    public CharClass CharClass { get; set; } = CharClass.Digit;
    public string CustomSet { get; set; } = "";
    public int Min { get; set; } = 1;
    public int? Max { get; set; } = 1;
}