namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexBang()
    {
        var start = _position;
        if (Match("!="))
        {
            return new SyntaxToken(SyntaxTokenKind.BangEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Bang, start, 1);
        }
    }
}