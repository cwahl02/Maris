namespace Maris.Compiler;

public readonly struct Token
{
    public TextSpan Span { get; }
    public TokenType Type { get; }
    public Token(TokenType type, TextSpan span)
    {
        Type = type;
        Span = span;
    }
}