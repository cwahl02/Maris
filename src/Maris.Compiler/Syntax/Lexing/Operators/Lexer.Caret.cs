namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexCaret()
    {
        var start = _iterator.Position;
        if (TryMatch("^="))
        {
            return new Token(TokenKind.CaretEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Caret, start, 1);
        }
    }
}