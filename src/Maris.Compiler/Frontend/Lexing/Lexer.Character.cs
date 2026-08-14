namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private Token LexCharacter()
    {
        var start = Position;
        var type = TokenType.CharacterLiteral;
        Advance(); // Skip the opening quote

        if (IsAtEnd)
        {
            type = TokenType.Invalid;
        }
        else if (Current == '\\' && Peek(1) == '\'')
        {
            Advance(2); // Skip the escaped quote
        }
        else
        {
            Advance();
        }

        if (IsAtEnd || Current != '\'')
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