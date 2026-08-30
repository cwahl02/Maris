namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexGreater()
    {
        var start = _iterator.Position;
        if (TryMatch(">>="))
        {
            return new SyntaxToken(SyntaxTokenKind.RightShiftEqual, start, 3);
        }
        else if (TryMatch(">="))
        {
            return new SyntaxToken(SyntaxTokenKind.GreaterThanEqual, start, 2);
        }
        else if (TryMatch(">>"))
        {
            return new SyntaxToken(SyntaxTokenKind.RightShift, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.GreaterThan, start, 1);
        }
    }
}