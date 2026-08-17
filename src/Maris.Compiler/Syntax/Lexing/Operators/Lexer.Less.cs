namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexLess()
    {
        var start = _iterator.Position;
        if (TryMatch("<<="))
        {
            return new Token(TokenKind.LeftShiftEqual, start, 3);
        }
        else if (TryMatch("<<"))
        {
            return new Token(TokenKind.LeftShift, start, 2);
        }
        else if (TryMatch("<="))
        {
            return new Token(TokenKind.LessEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Less, start, 1);
        }
    }
}