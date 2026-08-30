namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexEqual()
    {
        var start = _position;
        if (Match("=="))
        {
            return new SyntaxToken(SyntaxTokenKind.EqualEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Equal, start, 1);
        }
    }
}