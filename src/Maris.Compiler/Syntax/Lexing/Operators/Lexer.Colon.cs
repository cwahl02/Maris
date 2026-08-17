namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexColon()
    {
        var start = _iterator.Position;
        if (TryMatch("::="))
        {
            return new Token(TokenKind.ColonColonEqual, start, 3);
        }
        else if (TryMatch("::"))
        {
            return new Token(TokenKind.ColonColon, start, 2);
        }
        else if (TryMatch(":="))
        {
            return new Token(TokenKind.ColonEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Colon, start, 1);
        }
    }
}