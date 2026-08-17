namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexAmpersand()
    {
        var start = _iterator.Position;
        if (TryMatch("&&"))
        {
            return new Token(TokenKind.AmpersandAmpersand, start, 2);
        }
        else if (TryMatch("&="))
        {
            return new Token(TokenKind.AmpersandEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Ampersand, start, 1);
        }
    }
}