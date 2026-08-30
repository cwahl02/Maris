namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexGreater()
    {
        var start = _position;
        if (Match(">>="))
        {
            return new SyntaxToken(SyntaxTokenKind.RightShiftEqual, start, 3);
        }
        else if (Match(">="))
        {
            return new SyntaxToken(SyntaxTokenKind.GreaterThanEqual, start, 2);
        }
        else if (Match(">>"))
        {
            return new SyntaxToken(SyntaxTokenKind.RightShift, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.GreaterThan, start, 1);
        }
    }
}