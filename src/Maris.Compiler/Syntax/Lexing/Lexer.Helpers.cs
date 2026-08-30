namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private void SkipTrivia()
    {
        while (!IsAtEnd)
        {
            if (char.IsWhiteSpace(Current))
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
        if (!Check("//"))
            return false;

        Advance(2); // Skip the '//' characters

        while (!IsAtEnd && Current != '\n')
        {
            Advance();
        }

        return true;
    }

    private bool TrySkipBlockComment()
    {
        if (!Check("/*"))
            return false;

        Advance(2); // Skip the '/*' characters

        int depth = 1; // Track the depth of nested block comments

        while (!IsAtEnd && depth > 0)
        {
            if (Check("/*"))
            {
                depth++;
                Advance(2); // Skip the '/*' characters
            }
            else if (Check("*/"))
            {
                depth--;
                Advance(2); // Skip the '*/' characters
            }
            else
            {
                Advance();
            }
        }

        return true;
    }

    private SyntaxToken LexSingle(SyntaxTokenKind kind)
    {
        var start = _position;
        Advance();
        return new SyntaxToken(kind, start, 1);
    }
}