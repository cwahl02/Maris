namespace Maris.Compiler;

public sealed partial class Lexer
{
    private Token MakeToken(TokenType type, int start, int length)
        => new Token(type, new TextSpan(start, length));

    private bool TryMatch(ReadOnlySpan<char> expected)
    {
        for (int i = 0; i < expected.Length; i++)
        {
            if (_window.Peek(i) != expected[i])
                return false;
        }

        _window.Advance(expected.Length);
        return true;
    }

    private void SkipTrivia()
    {
        while (!_window.EndOfText())
        {
            if (char.IsWhiteSpace(_window.Current))
            {
                _window.Advance();
                continue;
            }

            if (TrySkipLineComment())
                continue;

            if (TrySkipBlockComment())
                continue;

            break;
        }
    }

    private bool TrySkipLineComment()
    {
        if (_window.Current != '/' || _window.Peek(1) != '/')
            return false;

        _window.Advance(2);

        while (!_window.EndOfText() && _window.Current != '\n')
            _window.Advance();

        return true;
    }

    private bool TrySkipBlockComment()
    {
        if (_window.Current != '/' || _window.Peek(1) != '*')
            return false;

        _window.Advance(2);

        int depth = 1;

        while (!_window.EndOfText() && depth > 0)
        {
            if (_window.Current == '/' && _window.Peek(1) == '*')
            {
                depth++;
                _window.Advance(2);
                continue;
            }

            if (_window.Current == '*' && _window.Peek(1) == '/')
            {
                depth--;
                _window.Advance(2);
                continue;
            }

            _window.Advance();
        }

        if (depth != 0)
            throw new Exception("Unterminated block comment");

        return true;
    }
}