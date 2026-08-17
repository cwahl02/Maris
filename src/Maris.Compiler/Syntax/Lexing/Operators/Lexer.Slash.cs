namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexSlash()
    {
        var start = _iterator.Position;
        if (TryMatch("/="))
        {
            return new Token(TokenKind.SlashEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Slash, start, 1);
        }
    }
}