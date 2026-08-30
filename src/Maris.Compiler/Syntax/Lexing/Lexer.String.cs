namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexString()
    {
        var start = _iterator.Position;
        var type = SyntaxTokenKind.StringLiteral;
        _iterator.Forward(); // Skip the opening quote

        while (!_iterator.IsAtEnd && _iterator.Current != '"')
        {
            if (_iterator.Current == '\\' && _iterator.Peek(1) == '"')
            {
                _iterator.Forward(2); // Skip the escaped quote
            }
            else
            {
                _iterator.Forward();
            }
        }

        if (_iterator.IsAtEnd)
        {
            type = SyntaxTokenKind.Invalid;
        }
        else
        {
            _iterator.Forward(); // Skip the closing quote
        }

        var length = _iterator.Position - start;
        return new SyntaxToken(type, start, length);
    }
}