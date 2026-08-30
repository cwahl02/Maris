namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexStar()
    {
        var start = _position;
        if (Match("*="))
        {
            return new SyntaxToken(SyntaxTokenKind.StarEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Star, start, 1);
        }
    }
}