namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexEqual()
    {
        var start = _iterator.Position;
        if (TryMatch("=="))
        {
            return new Token(TokenKind.EqualEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Equal, start, 1);
        }
    }
}