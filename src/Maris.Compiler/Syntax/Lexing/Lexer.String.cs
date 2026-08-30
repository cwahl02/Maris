namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexString()
    {
        var start = _position;
        var type = SyntaxTokenKind.StringLiteral;
        Advance(); // Skip the opening quote

        while (!IsAtEnd && Current != '"')
        {
            if (Current == '\\' && Peek(1) == '"')
            {
                Advance(2); // Skip the escaped quote
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
            Advance(); // Skip the closing quote
        }

        var length = _position - start;
        return new SyntaxToken(type, start, length);
    }
}