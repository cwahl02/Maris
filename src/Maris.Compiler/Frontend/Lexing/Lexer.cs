namespace Maris.Compiler.Lexing;

using Maris.Core.Text;

public sealed partial class Lexer(string text)
{
    private readonly string _text = text;
    private readonly TextWindow _textWindow = new(text);
    private int Position => _textWindow.Position;
    private char Current => _textWindow.Current;
    private char Peek(int offset) => _textWindow.Peek(offset);
    private bool IsAtEnd => _textWindow.IsAtEnd;
    private void Advance() => _textWindow.Advance();
    private void Advance(int count) => _textWindow.Advance(count);

    public List<Token> Lex()
    {
        var tokens = new List<Token>();

        while (!IsAtEnd)
        {
            SkipTrivia();

            if (IsAtEnd)
            {
                break;
            }

            tokens.Add(LexToken());
        }

        tokens.Add(MakeToken(TokenType.EOF, Position, 0));

        return tokens;
    }

    private Token LexToken()
    {
        if (IsIdentifierStart(Current))
        {
            return LexIdentifier();
        }
        else if (Char.IsDigit(Current))
        {
            return LexNumber();
        }
        else if (Current == '"')
        {
            return LexString();
        }
        else if (Current == '\'')
        {
            return LexCharacter();
        }
        else if (Current == '.' && Char.IsDigit(Peek(1)))
        {
            return LexNumber();
        }


        return Current switch
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