namespace Maris.Compiler.Lexer;

using Maris.Core.Text;

public sealed partial class Lexer
{
    private string _text;
    private readonly TextWindow _textWindow;
    private int _position => _textWindow.Position;
    private char _current => _textWindow.Current;
    private char _peek(int offset) => _textWindow.Peek(offset);
    private bool _isAtEnd => _textWindow.IsAtEnd;
    private void _advance() => _textWindow.Advance();
    private void _advance(int count) => _textWindow.Advance(count);

    public Lexer(string text)
    {
        _text = text;
        _textWindow = new TextWindow(text);
    }

    public List<Token> Lex()
    {
        var tokens = new List<Token>();

        while (!_isAtEnd)
        {
            SkipTrivia();

            if (_isAtEnd)
            {
                break;
            }
            
            tokens.Add(LexToken());
        }

        tokens.Add(MakeToken(TokenType.EOF, _position, 0));

        return tokens;
    }

    private Token LexToken()
    {
        if (IsIdentifierStart(_current))
        {
            return LexIdentifier();
            // Add token to the list of tokens
        }
        else if (Char.IsDigit(_current))
        {
            return LexNumber();
        }
        else if (_current == '"')
        {
            return LexString();
        }
        else if (_current == '\'')
        {
            return LexCharacter();
        }
        else if (_current == '.' && Char.IsDigit(_peek(1)))
        {
            return LexNumber();
        }
    
        
        return _current switch
        {
            '+' => LexPlus(),
            '-' => LexMinus(),
            '*' => LexStar(),
            '/' => LexSlash(),
            '%' => LexPercent(),
            '=' => LexEqual(),

            '^' => LexCaret(),
            '&' => LexAmpersand(),
            '|' => LexPipe(),
            '!' => LexExclamation(),

            '<' => LexLess(),
            '>' => LexGreater(),

            '.' => LexDot(),
            ':' => LexColon(),

            // TODO: Add support for slice '[]u8' and array '[3]u8' types
            //'[' => LexLeftBracket(),

            ',' => LexSingle(TokenType.Comma),
            ';' => LexSingle(TokenType.Semicolon),
            '(' => LexSingle(TokenType.LeftParen),
            ')' => LexSingle(TokenType.RightParen),
            '[' => LexSingle(TokenType.LeftBracket),
            ']' => LexSingle(TokenType.RightBracket),
            '{' => LexSingle(TokenType.LeftBrace),
            '}' => LexSingle(TokenType.RightBrace),

            _ => LexUnknown()
        };
    }
}