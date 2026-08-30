namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexDot()
    {
        var start = _position;
        if (Match(".."))
        {
            return new SyntaxToken(SyntaxTokenKind.DotDot, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Dot, start, 1);
        }
    }
}