namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexColon()
    {
        var start = _position;
        if (Match("::="))
        {
            return new SyntaxToken(SyntaxTokenKind.ColonColonEqual, start, 3);
        }
        else if (Match("::"))
        {
            return new SyntaxToken(SyntaxTokenKind.ColonColon, start, 2);
        }
        else if (Match(":="))
        {
            return new SyntaxToken(SyntaxTokenKind.ColonEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Colon, start, 1);
        }
    }
}