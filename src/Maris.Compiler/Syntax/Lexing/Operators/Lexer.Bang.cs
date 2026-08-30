namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexBang()
    {
        var start = _iterator.Position;
        if (TryMatch("!="))
        {
            return new SyntaxToken(SyntaxTokenKind.BangEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Bang, start, 1);
        }
    }
}