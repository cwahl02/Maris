namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexCaret()
    {
        var start = _position;
        if (Match("^="))
        {
            return new SyntaxToken(SyntaxTokenKind.CaretEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Caret, start, 1);
        }
    }
}