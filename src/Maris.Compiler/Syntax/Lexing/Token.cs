using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed record SyntaxToken(
    SyntaxTokenKind Kind,
    TextSpan Span
)
{
    public static readonly SyntaxToken Eof = new(SyntaxTokenKind.Eof, TextSpan.Empty);
    public SyntaxToken(SyntaxTokenKind kind, int start, int length) : this(kind, new TextSpan(start, length)) { }
}

public static class TokenListExtensions
{
    public static bool Compare(
        this IReadOnlyList<SyntaxToken> expected,
        IReadOnlyList<SyntaxToken> actual
    )
    {
        if (expected.Count != actual.Count)
            return false;

        for (int i = 0; i < expected.Count; i++)
        {
            if(!expected[i].Equals(actual[i]))
                return false;
        }

        return true;
    }

    public static bool Contains(
        this IReadOnlyList<SyntaxToken> actual,
         params SyntaxTokenKind[] kinds)
    {
        HashSet<SyntaxTokenKind> set = new();

        foreach (SyntaxToken token in actual)
        {
            set.Add(token.Kind);    
        }

        foreach (SyntaxTokenKind kind in kinds)
        {
            if (set.Contains(kind))
                return true;
        }

        return false;
    }

    public static bool Contains(
        this IReadOnlyList<SyntaxToken> actual,
        string sourceText,
        params string[] texts
    )
    {
        HashSet<string> set = new();

        foreach (SyntaxToken token in actual)
        {
            set.Add(sourceText.Substring(token.Span.Start, token.Span.Length));    
        }

        foreach (string text in texts)
        {
            if (set.Contains(text))
                return true;
        }

        return false;
    }
}