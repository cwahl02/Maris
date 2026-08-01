namespace Maris.Compiler;

public sealed partial class Lexer
{
    private readonly TextWindow _window;

    public Lexer(SourceFile source)
    {
        _window = new TextWindow(source.Text);
    }

    public List<Token> Tokenize()
    {
        var tokens = new List<Token>();

        while (!_window.EndOfText())
        {
            SkipTrivia();

            if (_window.EndOfText())
                break;

            tokens.Add(LexToken());
        }

        tokens.Add(new Token(
            TokenType.EndOfFile,
            new TextSpan(_window.Position, 0)));

        return tokens;
    }

    private Token LexToken()
    {
        if (_window.Current == '_' || char.IsLetter(_window.Current))
            return LexIdentifier();

        if (char.IsDigit(_window.Current))
            return LexNumber();

        if (_window.Current == '"')
            return LexStringLiteral();

        if (_window.Current == '\'')
            return LexCharacterLiteral();

        if (_window.Current == '.' && char.IsDigit(_window.Peek(1)))
            return LexNumber();

        return _window.Current switch
        {
            '+' => LexPlus(),
            '-' => LexMinus(),
            '*' => LexStar(),
            '/' => LexSlash(),
            '%' => LexPercent(),

            '(' => LexSingle(TokenType.LeftParen),
            ')' => LexSingle(TokenType.RightParen),

            '[' => LexSingle(TokenType.LeftBracket),
            ']' => LexSingle(TokenType.RightBracket),

            '{' => LexSingle(TokenType.LeftBrace),
            '}' => LexSingle(TokenType.RightBrace),

            '.' => LexDot(TokenType.Dot),
            ',' => LexSingle(TokenType.Comma),
            ';' => LexSingle(TokenType.Semicolon),
            ':' => LexColon(),

            '<' => LexLess(),
            '>' => LexGreater(),
            '=' => LexEqual(), 

            '!' => LexBang(),
            '&' => LexAmpersand(),
            '|' => LexPipe(),
            '^' => LexCaret(),
            '~' => LexSingle(TokenType.Tilde),
            '?' => LexSingle(TokenType.Question),

            '@' => LexSingle(TokenType.At),
            '#' => LexSingle(TokenType.Hash),

            _ => LexUnknown()
        };
    }

    private Token LexSingle(TokenType type)
    {
        int start = _window.Position;

        _window.Advance();

        return MakeToken(type, start, 1);
    } 

    private Token LexUnknown()
    {
        int start = _window.Position;

        _window.Advance();

        return MakeToken(TokenType.Invalid, start, 1);
    }
}
