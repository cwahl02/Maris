namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexLess()
    {
        var start = _position;
        if (Match("<<="))
        {
            return new SyntaxToken(SyntaxTokenKind.LeftShiftEqual, start, 3);
        }
        else if (Match("<<"))
        {
            return new SyntaxToken(SyntaxTokenKind.LeftShift, start, 2);
        }
        else if (Match("<="))
        {
            return new SyntaxToken(SyntaxTokenKind.LessThanEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.LessThan, start, 1);
        }
    }
}