namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexLess()
    {
        var start = _iterator.Position;
        if (TryMatch("<<="))
        {
            return new SyntaxToken(SyntaxTokenKind.LeftShiftEqual, start, 3);
        }
        else if (TryMatch("<<"))
        {
            return new SyntaxToken(SyntaxTokenKind.LeftShift, start, 2);
        }
        else if (TryMatch("<="))
        {
            return new SyntaxToken(SyntaxTokenKind.LessThanEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.LessThan, start, 1);
        }
    }
}