using System.Text.RegularExpressions;

namespace Regex.Core;

public interface IRegexTester
{
    TestResult Test(string pattern, RegexOptions options, string input);
}


public class RegexTester : IRegexTester
{
    private static readonly TimeSpan MatchTimeout = TimeSpan.FromMilliseconds(250);

    public TestResult Test(string pattern, RegexOptions options, string input)
    {
        if (string.IsNullOrEmpty(pattern))
            return new TestResult(true, false, Array.Empty<string>(), null);

        try
        {
            var regex = new System.Text.RegularExpressions.Regex(pattern, options, MatchTimeout);
            var matches = regex.Matches(input).Select(m => m.Value).ToList();
            return new TestResult(true, matches.Count > 0, matches, null);
        }
        catch (RegexParseException ex)
        {
            return new TestResult(false, false, Array.Empty<string>(), ex.Message);
        }
        catch (RegexMatchTimeoutException)
        {
            return new TestResult(true, false, Array.Empty<string>(),
                "Matching timed out — possible catastrophic backtracking.");
        }
    }
}