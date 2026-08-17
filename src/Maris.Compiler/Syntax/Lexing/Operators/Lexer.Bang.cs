namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexBang()
    {
        var start = _iterator.Position;
        if (TryMatch("!="))
        {
            return new Token(TokenKind.BangEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Bang, start, 1);
        }
    }
}