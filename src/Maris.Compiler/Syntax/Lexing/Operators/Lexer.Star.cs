namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexStar()
    {
        var start = _iterator.Position;
        if (TryMatch("*="))
        {
            return new Token(TokenKind.StarEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Star, start, 1);
        }
    }
}