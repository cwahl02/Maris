namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexSlash()
    {
        var start = _iterator.Position;
        if (TryMatch("/="))
        {
            return new SyntaxToken(SyntaxTokenKind.SlashEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Slash, start, 1);
        }
    }
}