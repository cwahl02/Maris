namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexMinus()
    {
        var start = _iterator.Position;
        if (TryMatch("--"))
        {
            return new Token(TokenKind.MinusMinus, start, 2);
        }
        else if (TryMatch("->"))
        {
            return new Token(TokenKind.Arrow, start, 2);
        }
        else if (TryMatch("-="))
        {
            return new Token(TokenKind.MinusEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Minus, start, 1);
        }
    }
}