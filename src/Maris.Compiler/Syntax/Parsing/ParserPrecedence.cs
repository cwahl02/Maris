using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

internal static class ParserPrecedence
{
    public static int GetUnaryPrecedence(TokenKind kind)
    {
        return kind switch
        {
            TokenKind.Plus => 6,
            TokenKind.Minus => 6,
            TokenKind.Bang => 6,
            TokenKind.Tilde => 6,

            _ => 0,
        };
    }
    public static int GetBinaryOperatorPrecedence(TokenKind kind)
    {
        return kind switch
        {
            TokenKind.Star => 5,
            TokenKind.Slash => 5,
            TokenKind.Percent => 5,

            TokenKind.Plus => 4,
            TokenKind.Minus => 4,

            TokenKind.Less => 3,
            TokenKind.LessEqual => 3,
            TokenKind.Greater => 3,
            TokenKind.GreaterEqual => 3,

            TokenKind.EqualEqual => 2,
            TokenKind.BangEqual => 2,

            TokenKind.AmpersandAmpersand => 1,
            TokenKind.PipePipe => 1,

            _ => 0,
        };
    }
}