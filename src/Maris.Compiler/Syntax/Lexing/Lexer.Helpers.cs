namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private void SkipTrivia()
    {
        while (!_iterator.IsAtEnd)
        {
            if (char.IsWhiteSpace(_iterator.Current))
            {
                _iterator.Forward();
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
        if (_iterator.Current != '/' || _iterator.Peek(1) != '/')
            return false;

        _iterator.Forward(2); // Skip the '//' characters

        while (!_iterator.IsAtEnd && _iterator.Current != '\n')
        {
            _iterator.Forward();
        }

        return true;
    }

    private bool TrySkipBlockComment()
    {
        if (_iterator.Current != '/' || _iterator.Peek(1) != '*')
            return false;

        _iterator.Forward(2); // Skip the '/*' characters

        int depth = 1; // Track the depth of nested block comments

        while (!_iterator.IsAtEnd && depth > 0)
        {
            if (_iterator.Current == '/' && _iterator.Peek(1) == '*')
            {
                depth++;
                _iterator.Forward(2); // Skip the '/*' characters
            }
            else if (_iterator.Current == '*' && _iterator.Peek(1) == '/')
            {
                depth--;
                _iterator.Forward(2); // Skip the '*/' characters
            }
            else
            {
                _iterator.Forward();
            }
        }

        return true;
    }

    private bool TryMatch(string expected)
    {
        for (int i = 0; i < expected.Length; i++)
        {
            if (_iterator.Peek(i) != expected[i])
                return false;
        }

        _iterator.Forward(expected.Length);
        return true;
    }

    private SyntaxToken LexSingle(SyntaxTokenKind kind)
    {
        var start = _iterator.Position;
        _iterator.Forward();
        return new SyntaxToken(kind, start, 1);
    }
}