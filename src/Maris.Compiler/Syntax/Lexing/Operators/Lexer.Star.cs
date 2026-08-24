namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexStar()
    {
        var start = _iterator.Position;
        if (TryMatch("*="))
        {
            return new SyntaxToken(SyntaxTokenKind.StarEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Star, start, 1);
        }
    }
}