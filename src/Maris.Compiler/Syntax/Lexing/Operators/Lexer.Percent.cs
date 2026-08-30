namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexPercent()
    {
        var start = _iterator.Position;
        if (TryMatch("%="))
        {
            return new SyntaxToken(SyntaxTokenKind.PercentEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Percent, start, 1);
        }
    }
}