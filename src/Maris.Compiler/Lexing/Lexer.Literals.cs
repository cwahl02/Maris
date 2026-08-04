using Maris.Compiler.Text;

namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private Token LexCharacterLiteral()
    {
        int start = _window.Position;

        _window.Advance(); // Skip the opening quote

        if (!_window.EndOfText() && _window.Current != '\'')
        {
            if (_window.Current == '\\')
            {
                _window.Advance(2); // Skip escaped quote
            }
            else
            {
                _window.Advance();
            }
        }

        if (_window.Current == '\'')
            _window.Advance(); // Skip the closing quote

        return MakeToken(TokenType.CharacterLiteral, start, _window.Position - start);
    }
    private Token LexStringLiteral()
    {
        int start = _window.Position;

        _window.Advance(); // Skip the opening quote

        while (!_window.EndOfText() && _window.Current != '"')
        {
            if (_window.Current == '\\')
            {
                _window.Advance();
                if(!_window.EndOfText())
                    _window.Advance(); // Skip escaped character
                    
                continue;
            }

            _window.Advance();
        }

        if (_window.Current == '"')
            _window.Advance(); // Skip the closing quote

        return MakeToken(TokenType.StringLiteral, start, _window.Position - start);
    }
}