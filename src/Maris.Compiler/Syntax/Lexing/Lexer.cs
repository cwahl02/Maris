using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private readonly SourceFile _sourceFile;
    private readonly string _text;
    private int _position;

    public Lexer(SourceFile sourceFile)
    {
        _sourceFile = sourceFile;
        _text = sourceFile.Text;
    }

    public List<SyntaxToken> Lex()
    {
        List<SyntaxToken> tokens = new();

        while (!IsAtEnd)
        {
            SkipTrivia();

            if (IsAtEnd)
            {
                break;
            }

            tokens.Add(LexToken());
        }

        tokens.Add(SyntaxToken.Eof);

        return tokens;
    }

    // ==================== Core Combinators ====================

    private bool IsAtEnd => _position >= _text.Length;

    private char Current => Peek(0);

    private char Peek(int offset)
    {
        int index = _position + offset;
        return index >= 0 && index < _text.Length ? _text[index] : '\0';
    }

    private bool Check(char c) => Current == c;

    private bool Check(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (Peek(i) != text[i])
            {
                return false;
            }
        }

        return true;
    }

    private void Advance() => Advance(1);

    private void Advance(int count)
    {
        for (int i = 0; i < count && !IsAtEnd; i++)
        {
            _position++;
        }
    }

    private bool Match(string text)
    {
        if (!Check(text))
        {
            return false;
        }

        Advance(text.Length);
        return true;
    }

    private SyntaxToken LexToken()
    {
        if (char.IsAsciiLetter(Current) || (Current == '_' && char.IsAsciiLetterOrDigit(Peek(1))))
        {
            return LexIdentifier();
        }
        else if (char.IsDigit(Current))
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

        return Current switch
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
