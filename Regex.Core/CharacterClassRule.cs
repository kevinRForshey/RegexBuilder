using System.Text;

namespace Regex.Core;

public sealed class CharacterClassRule (CharClass type, Quantifier quantifier, string? customSet = null) : IRegexRule
{
    public string Build()
    {
        var token = type switch
        {
           CharClass.Digit => @"\d",
           CharClass.Letter => "[A-Za-z]",
           CharClass.LetterUpper => "[A-Z]",
           CharClass.LetterLower => "[a-z]",
           CharClass.Alphanumeric => "[A-Za-z0-9]",
           CharClass.Word => @"\w",
           CharClass.Whitespace => @"\s",
           CharClass.AnyChar => ".",
           CharClass.Custom => $"[{EscapeInClass(customSet ?? "")}]",
           _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported character class: {type}")
        };
        return token + quantifier.Build();
    }
    
    public string Describe()
    { 
        var noun = type switch
        {
            CharClass.Digit => "digit",
            CharClass.Letter => "letter",
            CharClass.LetterUpper => "uppercase letter",
            CharClass.LetterLower => "lowercase letter",
            CharClass.Alphanumeric => "alphanumeric character",
            CharClass.Word => "word character",
            CharClass.Whitespace => "whitespace character",
            CharClass.AnyChar => "any character",
            CharClass.Custom => "custom character class",
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported character class: {type}")
        };
        return $"{noun} {quantifier.Describe()}";
    }

    private static string EscapeInClass(string s)
    {
        var sb = new StringBuilder();
        foreach (var c in s)
        {
           if ( c is '\\' or '^' or '-') sb.Append('\\');
           sb.Append(c);
        }
        return sb.ToString();
    }
}