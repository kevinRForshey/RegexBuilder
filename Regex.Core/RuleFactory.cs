namespace Regex.Core;

public interface IRuleFactory
{
    IRegexRule Create(RuleDraft draft);
}

public sealed class RuleFactory : IRuleFactory
{
    public IRegexRule Create(RuleDraft d) => d.Kind switch
    {
        RuleKind.Literal        => new LiteralRule(d.Text, new Quantifier(d.Min, d.Max)),
        RuleKind.CharacterClass => new CharacterClassRule(d.CharClass, new Quantifier(d.Min, d.Max), d.CustomSet),
        RuleKind.RawPattern     => new RawRule(d.Text, $"the pattern \u201c{d.Text}\u201d"),
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
}