namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexAmpersand()
    {
        var start = _iterator.Position;
        if (TryMatch("&&"))
        {
            return new SyntaxToken(SyntaxTokenKind.AmpersandAmpersand, start, 2);
        }
        else if (TryMatch("&="))
        {
            return new SyntaxToken(SyntaxTokenKind.AmpersandEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Ampersand, start, 1);
        }
    }
}