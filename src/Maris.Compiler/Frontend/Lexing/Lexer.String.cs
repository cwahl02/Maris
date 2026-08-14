namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private Token LexString()
    {
        var start = Position;
        var type = TokenType.StringLiteral;
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
            type = TokenType.Invalid;
        }
        else
        {
            Advance(); // Skip the closing quote
        }

        var length = Position - start;
        return MakeToken(type, start, length);
    }
}