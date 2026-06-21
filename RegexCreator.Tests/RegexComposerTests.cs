using Regex.Core;       
using Xunit;
namespace RegexCreator.Tests;

public class RegexComposerTests
{
    private readonly IRegexComposer _composer = new RegexComposer();

    [Fact]
    public void Anchored_exactly_three_digits()
    {
        IReadOnlyList<IRegexRule> rules =
            [ new CharacterClassRule(CharClass.Digit, new Quantifier(3, 3)) ];

        var result = _composer.Compose(rules,
            new RegexBuildOptions { AnchorStart = true, AnchorEnd = true });

        Assert.Equal(@"^\d{3}$", result.Pattern);
    }

    [Theory]
    [InlineData(1, 1, "")]
    [InlineData(0, 1, "?")]
    [InlineData(0, null, "*")]
    [InlineData(1, null, "+")]
    [InlineData(2, 5, "{2,5}")]
    public void Quantifier_renders_correctly(int min, int? max, string expected)
        => Assert.Equal(expected, new Quantifier(min, max).Build());
}