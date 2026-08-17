namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexDot()
    {
        var start = _iterator.Position;
        if (TryMatch(".."))
        {
            return new Token(TokenKind.DotDot, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Dot, start, 1);
        }
    }
}