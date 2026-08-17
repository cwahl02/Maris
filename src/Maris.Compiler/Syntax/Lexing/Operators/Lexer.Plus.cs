namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexPlus()
    {
        var start = _iterator.Position;
        if (TryMatch("++"))
        {
            return new Token(TokenKind.PlusPlus, start, 2);
        }
        else if (TryMatch("+="))
        {
            return new Token(TokenKind.PlusEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Plus, start, 1);
        }
    }

}