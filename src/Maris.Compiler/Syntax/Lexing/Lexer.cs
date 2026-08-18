using Maris.Core.Iterator;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private readonly SourceFile _sourceFile;
    private readonly Iterator<char> _iterator;

    public Lexer(SourceFile sourceFile)
    {
        _sourceFile = sourceFile;
        _iterator = new Iterator<char>(sourceFile.Text.ToCharArray());
    }

    public List<Token> Lex()
    {
        List<Token> tokens = new();

        while (!_iterator.IsAtEnd)
        {
            SkipTrivia();

            tokens.Add(LexToken());
        }

        tokens.Add(Token.Eof);

        return tokens;
    }

    private Token LexToken()
    {
        if (char.IsAsciiLetter(_iterator.Current) || (_iterator.Current == '_' && char.IsAsciiLetterOrDigit(_iterator.Peek(1))))
        {
            return LexIdentifier();
        }
        else if (char.IsDigit(_iterator.Current))
        {
            return LexNumber();
        }
        else if (_iterator.Current == '"')
        {
            return LexString();
        }
        else if (_iterator.Current == '\'')
        {
            return LexCharacter();
        }
        
        return _iterator.Current switch
        {
            '&' => LexAmpersand(),
            '!' => LexBang(),
            '^' => LexCaret(),
            ':' => LexColon(),
            '.' => LexDot(),
            '=' => LexEqual(),
            '>' => LexGreater(),
            '<' => LexLess(),
            '-' => LexMinus(),
            '%' => LexPercent(),
            '|' => LexPipe(),
            '+' => LexPlus(),
            '/' => LexSlash(),
            '*' => LexStar(),

            '~' => LexSingle(TokenKind.Tilde),
            ',' => LexSingle(TokenKind.Comma),
            ';' => LexSingle(TokenKind.Semicolon),
            '(' => LexSingle(TokenKind.LeftParen),
            ')' => LexSingle(TokenKind.RightParen),
            '{' => LexSingle(TokenKind.LeftBrace),
            '}' => LexSingle(TokenKind.RightBrace),
            '[' => LexSingle(TokenKind.LeftBracket),
            ']' => LexSingle(TokenKind.RightBracket),
            '_' => LexSingle(TokenKind.Underscore),
            _ => LexSingle(TokenKind.Invalid)
        };
    }
}