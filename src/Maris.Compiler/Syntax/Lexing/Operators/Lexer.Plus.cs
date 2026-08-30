namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexPlus()
    {
        var start = _iterator.Position;
        if (TryMatch("++"))
        {
            return new SyntaxToken(SyntaxTokenKind.PlusPlus, start, 2);
        }
        else if (TryMatch("+="))
        {
            return new SyntaxToken(SyntaxTokenKind.PlusEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Plus, start, 1);
        }
    }

}