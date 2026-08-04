using Maris.Compiler.Text;

namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private Token LexEqual()
    {
        int start = _window.Position;

        if (TryMatch("=="))
            return MakeToken(TokenType.EqualEqual, start, 2);

        _window.Advance(); // consume the '='
        return MakeToken(TokenType.Equal, start, 1);
    }

    private Token LexPlus()
    {
        int start = _window.Position;

        if(TryMatch("++"))
            return MakeToken(TokenType.PlusPlus, start, 2);

        if (TryMatch("+="))
            return MakeToken(TokenType.PlusEqual, start, 2);

        _window.Advance(); // consume the '+'
        return MakeToken(TokenType.Plus, start, 1);
    }

    private Token LexMinus()
    {
        int start = _window.Position;
        if (TryMatch("--"))
            return MakeToken(TokenType.MinusMinus, start, 2);

        if (TryMatch("-="))
            return MakeToken(TokenType.MinusEqual, start, 2);

        if (TryMatch("->"))
            return MakeToken(TokenType.Arrow, start, 2);

        _window.Advance(); // consume the '-'
        return MakeToken(TokenType.Minus, start, 1);
    }

    private Token LexStar()
    {
        int start = _window.Position;

        if (TryMatch("*="))
            return MakeToken(TokenType.StarEqual, start, 2);

        _window.Advance(); // consume the '*'
        return MakeToken(TokenType.Star, start, 1);
    }

    private Token LexSlash()
    {
        int start = _window.Position;

        if (TryMatch("/="))
            return MakeToken(TokenType.SlashEqual, start, 2);

        _window.Advance(); // consume the '/'
        return MakeToken(TokenType.Slash, start, 1);
    }

    private Token LexPercent()
    {
        int start = _window.Position;

        if (TryMatch("%="))
            return MakeToken(TokenType.PercentEqual, start, 2);

        _window.Advance(); // consume the '%'
        return MakeToken(TokenType.Percent, start, 1);
    }

    private Token LexCaret()
    {
        int start = _window.Position;

        if (TryMatch("^="))
            return MakeToken(TokenType.CaretEqual, start, 2);

        _window.Advance(); // consume the '^'
        return MakeToken(TokenType.Caret, start, 1);
    }

    private Token LexAmpersand()
    {
        int start = _window.Position;

        if (TryMatch("&&"))
            return MakeToken(TokenType.AmpersandAmpersand, start, 2);

        if (TryMatch("&="))
            return MakeToken(TokenType.AmpersandEqual, start, 2);

        _window.Advance(); // consume the '&'
        return MakeToken(TokenType.Ampersand, start, 1);
    }

    private Token LexPipe()
    {
        int start = _window.Position;

        if (TryMatch("||"))
            return MakeToken(TokenType.PipePipe, start, 2);

        if (TryMatch("|="))
            return MakeToken(TokenType.PipeEqual, start, 2);

        _window.Advance(); // consume the '|'
        return MakeToken(TokenType.Pipe, start, 1);
    }

    private Token LexBang()
    {
        int start = _window.Position;

        if (TryMatch("!="))
            return MakeToken(TokenType.BangEqual, start, 2);

        _window.Advance(); // consume the '!'
        return MakeToken(TokenType.Bang, start, 1);
    }

    private Token LexLess()
    {
        int start = _window.Position;

        if (TryMatch("<<="))
            return MakeToken(TokenType.LeftShiftEqual, start, 3); // '<<=' is 3 characters

        if (TryMatch("<="))
            return MakeToken(TokenType.LessEqual, start, 2);
        
        if (TryMatch("<<"))
            return MakeToken(TokenType.LeftShift, start, 2);

        _window.Advance(); // consume the '<'
        return MakeToken(TokenType.Less, start, 1);
    }

    private Token LexGreater()
    {
        int start = _window.Position;

        if (TryMatch(">>="))
            return MakeToken(TokenType.RightShiftEqual, start, 3); // '>>=' is 3 characters

        if (TryMatch(">="))
            return MakeToken(TokenType.GreaterEqual, start, 2);
        
        if (TryMatch(">>"))
            return MakeToken(TokenType.RightShift, start, 2);

        _window.Advance(); // consume the '>'
        return MakeToken(TokenType.Greater, start, 1);
    }

    private Token LexDot(TokenType type)
    {
        int start = _window.Position;

        if (TryMatch(".."))
            return MakeToken(TokenType.Range, start, 2);

        _window.Advance(); // consume the '.'
        return MakeToken(type, start, 1);
    }

    private Token LexColon()
    {
        int start = _window.Position;

        if (TryMatch("::="))
            return MakeToken(TokenType.ColonColonEqual, start, 3);

        if (TryMatch("::"))
            return MakeToken(TokenType.ColonColon, start, 2);

        if (TryMatch(":="))
            return MakeToken(TokenType.ColonEqual, start, 2);

        _window.Advance(); // consume the ':'
        return MakeToken(TokenType.Colon, start, 1);
    }
}