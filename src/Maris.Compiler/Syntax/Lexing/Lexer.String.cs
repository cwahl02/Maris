namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexString()
    {
        var start = _iterator.Position;
        var type = TokenKind.StringLiteral;
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
            type = TokenKind.Invalid;
        }
        else
        {
            _iterator.Forward(); // Skip the closing quote
        }

        var length = _iterator.Position - start;
        return new Token(type, start, length);
    }
}