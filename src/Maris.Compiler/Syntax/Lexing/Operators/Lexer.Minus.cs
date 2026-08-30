namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexMinus()
    {
        var start = _iterator.Position;
        if (TryMatch("--"))
        {
            return new SyntaxToken(SyntaxTokenKind.MinusMinus, start, 2);
        }
        else if (TryMatch("->"))
        {
            return new SyntaxToken(SyntaxTokenKind.Arrow, start, 2);
        }
        else if (TryMatch("-="))
        {
            return new SyntaxToken(SyntaxTokenKind.MinusEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Minus, start, 1);
        }
    }
}