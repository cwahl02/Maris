namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexMinus()
    {
        var start = _position;
        if (Match("--"))
        {
            return new SyntaxToken(SyntaxTokenKind.MinusMinus, start, 2);
        }
        else if (Match("->"))
        {
            return new SyntaxToken(SyntaxTokenKind.Arrow, start, 2);
        }
        else if (Match("-="))
        {
            return new SyntaxToken(SyntaxTokenKind.MinusEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Minus, start, 1);
        }
    }
}