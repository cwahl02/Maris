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