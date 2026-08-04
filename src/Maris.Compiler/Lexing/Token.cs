using Maris.Compiler.Text;

namespace Maris.Compiler.Lexing;

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