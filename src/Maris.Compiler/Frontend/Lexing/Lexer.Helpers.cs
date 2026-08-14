namespace Maris.Compiler.Lexing;

using Maris.Core.Text;

public sealed partial class Lexer
{
    private Token MakeToken(TokenType type, int start, int length)
        => new Token(type, start, length, _text);

    private void SkipTrivia()
    {
        while (!IsAtEnd)
        {
            if(char.IsWhiteSpace(Current))
            {
                Advance();
                continue;
            }

            if (TrySkipLineComment())
            {
                continue;
            }

            if (TrySkipBlockComment())
            {
                continue;
            }

            break;
        }
    }

    private bool TrySkipLineComment()
    {
        if (Current != '/' || Peek(1) != '/')
        {
            return false;
        }

        Advance(2); // Skip the '//' characters

        while (!IsAtEnd && Current != '\n')
        {
            Advance();
        }

        return true;
    }

    private bool TrySkipBlockComment()
    {
        if (Current != '/' || Peek(1) != '*')
        {
            return false;
        }

        Advance(2); // Skip the '/*' characters

        int depth = 1; // Track the depth of nested block comments
        while (!IsAtEnd && depth > 0)
        {
            if (Current == '/' && Peek(1) == '*')
            {
                Advance(2); // Skip the '/*' characters
                depth++;
            }
            if (Current == '*' && Peek(1) == '/')
            {
                Advance(2); // Skip the '*/' characters
                depth--;
                continue;
            }
            else
            {
                Advance();
            }
        }

        // If we reach here, it means we reached the end of the text without finding a closing '*/'
        // You might want to handle this case (e.g., report an error)
        return false;
    }

    private bool TryMatch(string expected)
    {
        for (int i = 0; i < expected.Length; i++)
        {
            if (Peek(i) != expected[i])
            {
                return false;
            }
        }

        Advance(expected.Length);
        return true;
    }

    private Token LexSingle(TokenType type)
    {
        var start = Position;
        Advance();
        return MakeToken(type, start, 1);
    }
    private Token LexUnknown()
    {
        var start = Position;
        Advance();
        return MakeToken(TokenType.Invalid, start, 1);
    }
}