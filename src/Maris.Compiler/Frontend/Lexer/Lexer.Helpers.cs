namespace Maris.Compiler.Lexer;

using Maris.Core.Text;

public sealed partial class Lexer
{
    private Token MakeToken(TokenType type, int start, int length)
        => new Token(type, start, length, _text);

    private void SkipTrivia()
    {
        while (!_isAtEnd)
        {
            if(char.IsWhiteSpace(_current))
            {
                _advance();
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
        if (_current != '/' || _peek(1) != '/')
        {
            return false;
        }

        _advance(2); // Skip the '//' characters

        while (!_isAtEnd && _current != '\n')
        {
            _advance();
        }

        return true;
    }

    private bool TrySkipBlockComment()
    {
        if (_current != '/' || _peek(1) != '*')
        {
            return false;
        }

        _advance(2); // Skip the '/*' characters

        int depth = 1; // Track the depth of nested block comments
        while (!_isAtEnd && depth > 0)
        {
            if (_current == '/' && _peek(1) == '*')
            {
                _advance(2); // Skip the '/*' characters
                depth++;
            }
            if (_current == '*' && _peek(1) == '/')
            {
                _advance(2); // Skip the '*/' characters
                depth--;
                continue;
            }
            else
            {
                _advance();
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
            if (_peek(i) != expected[i])
            {
                return false;
            }
        }

        _advance(expected.Length);
        return true;
    }

    private Token LexSingle(TokenType type)
    {
        var start = _position;
        _advance();
        return MakeToken(type, start, 1);
    }
    private Token LexUnknown()
    {
        var start = _position;
        _advance();
        return MakeToken(TokenType.Invalid, start, 1);
    }
}