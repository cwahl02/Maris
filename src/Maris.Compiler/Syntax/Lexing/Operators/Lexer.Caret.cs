namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexCaret()
    {
        var start = _iterator.Position;
        if (TryMatch("^="))
        {
            return new SyntaxToken(SyntaxTokenKind.CaretEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Caret, start, 1);
        }
    }
}