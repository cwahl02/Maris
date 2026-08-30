namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexCharacter()
    {
        var start = _position;
        var type = SyntaxTokenKind.CharacterLiteral;
        Advance(); // Skip the opening single quote

        while (!IsAtEnd && Current != '\'')
        {
            if (Current == '\\' && Peek(1) == '\'')
            {
                Advance(2); // Skip the escaped single quote
            }
            else
            {
                Advance();
            }
        }

        if (IsAtEnd)
        {
            type = SyntaxTokenKind.Invalid;
        }
        else
        {
            Advance(); // Skip the closing single quote
        }

        var length = _position - start;
        return new SyntaxToken(type, start, length);
    }
}