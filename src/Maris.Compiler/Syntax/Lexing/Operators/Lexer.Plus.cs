namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexPlus()
    {
        var start = _position;
        if (Match("++"))
        {
            return new SyntaxToken(SyntaxTokenKind.PlusPlus, start, 2);
        }
        else if (Match("+="))
        {
            return new SyntaxToken(SyntaxTokenKind.PlusEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Plus, start, 1);
        }
    }

}