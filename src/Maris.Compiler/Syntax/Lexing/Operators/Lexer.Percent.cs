namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexPercent()
    {
        var start = _position;
        if (Match("%="))
        {
            return new SyntaxToken(SyntaxTokenKind.PercentEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Percent, start, 1);
        }
    }
}