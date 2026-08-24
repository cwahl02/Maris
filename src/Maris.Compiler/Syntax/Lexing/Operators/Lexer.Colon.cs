namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexColon()
    {
        var start = _iterator.Position;
        if (TryMatch("::="))
        {
            return new SyntaxToken(SyntaxTokenKind.ColonColonEqual, start, 3);
        }
        else if (TryMatch("::"))
        {
            return new SyntaxToken(SyntaxTokenKind.ColonColon, start, 2);
        }
        else if (TryMatch(":="))
        {
            return new SyntaxToken(SyntaxTokenKind.ColonEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Colon, start, 1);
        }
    }
}