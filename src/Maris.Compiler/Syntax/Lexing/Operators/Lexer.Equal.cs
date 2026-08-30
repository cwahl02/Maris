namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexEqual()
    {
        var start = _iterator.Position;
        if (TryMatch("=="))
        {
            return new SyntaxToken(SyntaxTokenKind.EqualEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Equal, start, 1);
        }
    }
}