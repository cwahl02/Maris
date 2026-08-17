using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed record Token(
    TokenKind Kind,
    TextSpan Span
)
{
    public static readonly Token Eof = new(TokenKind.Eof, TextSpan.Empty);
    public Token(TokenKind kind, int start, int length) : this(kind, new TextSpan(start, length)) { }
}

public static class TokenListExtensions
{
    public static bool Compare(
        this IReadOnlyList<Token> expected,
        IReadOnlyList<Token> actual
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
        this IReadOnlyList<Token> actual,
         params TokenKind[] kinds)
    {
        HashSet<TokenKind> set = new();

        foreach (Token token in actual)
        {
            set.Add(token.Kind);    
        }

        foreach (TokenKind kind in kinds)
        {
            if (set.Contains(kind))
                return true;
        }

        return false;
    }

    public static bool Contains(
        this IReadOnlyList<Token> actual,
        string sourceText,
        params string[] texts
    )
    {
        HashSet<string> set = new();

        foreach (Token token in actual)
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