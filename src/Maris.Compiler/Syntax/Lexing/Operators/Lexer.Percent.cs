namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexPercent()
    {
        var start = _iterator.Position;
        if (TryMatch("%="))
        {
            return new Token(TokenKind.PercentEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Percent, start, 1);
        }
    }
}