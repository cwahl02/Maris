namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexDot()
    {
        var start = _iterator.Position;
        if (TryMatch(".."))
        {
            return new SyntaxToken(SyntaxTokenKind.DotDot, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Dot, start, 1);
        }
    }
}