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

    public List<SyntaxToken> Lex()
    {
        List<SyntaxToken> tokens = new();

        while (!_iterator.IsAtEnd)
        {
            SkipTrivia();

            tokens.Add(LexToken());
        }

        tokens.Add(SyntaxToken.Eof);

        return tokens;
    }

    private SyntaxToken LexToken()
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

            '~' => LexSingle(SyntaxTokenKind.Tilde),
            ',' => LexSingle(SyntaxTokenKind.Comma),
            ';' => LexSingle(SyntaxTokenKind.Semicolon),
            '(' => LexSingle(SyntaxTokenKind.LeftParen),
            ')' => LexSingle(SyntaxTokenKind.RightParen),
            '{' => LexSingle(SyntaxTokenKind.LeftBrace),
            '}' => LexSingle(SyntaxTokenKind.RightBrace),
            '[' => LexSingle(SyntaxTokenKind.LeftBracket),
            ']' => LexSingle(SyntaxTokenKind.RightBracket),
            '_' => LexSingle(SyntaxTokenKind.Underscore),
            _ => LexSingle(SyntaxTokenKind.Invalid)
        };
    }
}