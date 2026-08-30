namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexSlash()
    {
        var start = _position;
        if (Match("/="))
        {
            return new SyntaxToken(SyntaxTokenKind.SlashEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Slash, start, 1);
        }
    }
}