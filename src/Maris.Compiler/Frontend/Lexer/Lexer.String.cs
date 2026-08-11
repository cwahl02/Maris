namespace Maris.Compiler.Lexer;

public sealed partial class Lexer
{
    private Token LexString()
    {
        var start = _position;
        var type = TokenType.StringLiteral;
        _advance(); // Skip the opening quote

        while (!_isAtEnd && _current != '"')
        {
            if (_current == '\\' && _peek(1) == '"')
            {
                _advance(2); // Skip the escaped quote
            }
            else
            {
                _advance();
            }
        }

        if (_isAtEnd)
        {
            type = TokenType.Invalid;
        }
        else
        {
            _advance(); // Skip the closing quote
        }

        var length = _position - start;
        return MakeToken(type, start, length);
    }
}