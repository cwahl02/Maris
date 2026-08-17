namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexGreater()
    {
        var start = _iterator.Position;
        if (TryMatch(">>="))
        {
            return new Token(TokenKind.RightShiftEqual, start, 3);
        }
        else if (TryMatch(">="))
        {
            return new Token(TokenKind.GreaterEqual, start, 2);
        }
        else if (TryMatch(">>"))
        {
            return new Token(TokenKind.RightShift, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Greater, start, 1);
        }
    }
}