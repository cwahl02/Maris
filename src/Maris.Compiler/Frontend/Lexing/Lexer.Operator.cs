namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private Token LexPlus()
    {
        var start = Position;
        if (TryMatch("++"))
        {
            return MakeToken(TokenType.PlusPlus, start, 2);
        }
        else if (TryMatch("+="))
        {
            return MakeToken(TokenType.PlusEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Plus, start, 1);
        }
    }

    private Token LexMinus()
    {
        var start = Position;
        if (TryMatch("--"))
        {
            return MakeToken(TokenType.MinusMinus, start, 2);
        }
        else if (TryMatch("->"))
        {
            return MakeToken(TokenType.Arrow, start, 2);
        }
        else if (TryMatch("-="))
        {
            return MakeToken(TokenType.MinusEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Minus, start, 1);
        }
    }

    private Token LexStar()
    {
        var start = Position;
        if (TryMatch("*="))
        {
            return MakeToken(TokenType.StarEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Star, start, 1);
        }
    }

    private Token LexSlash()
    {
        var start = Position;
        if (TryMatch("/="))
        {
            return MakeToken(TokenType.SlashEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Slash, start, 1);
        }
    }

    private Token LexPercent()
    {
        var start = Position;
        if (TryMatch("%="))
        {
            return MakeToken(TokenType.PercentEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Percent, start, 1);
        }
    }

    private Token LexEqual()
    {
        var start = Position;
        if (TryMatch("=="))
        {
            return MakeToken(TokenType.EqualEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Equal, start, 1);
        }
    }

    private Token LexCaret()
    {
        var start = Position;
        if (TryMatch("^="))
        {
            return MakeToken(TokenType.CaretEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Caret, start, 1);
        }
    }

    private Token LexAmpersand()
    {
        var start = Position;
        if (TryMatch("&&"))
        {
            return MakeToken(TokenType.AmpersandAmpersand, start, 2);
        }
        else if (TryMatch("&="))
        {
            return MakeToken(TokenType.AmpersandEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Ampersand, start, 1);
        }
    }

    private Token LexPipe()
    {
        var start = Position;
        if (TryMatch("||"))
        {
            return MakeToken(TokenType.PipePipe, start, 2);
        }
        else if (TryMatch("|="))
        {
            return MakeToken(TokenType.PipeEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Pipe, start, 1);
        }
    }

    private Token LexExclamation()
    {
        var start = Position;
        if (TryMatch("!="))
        {
            return MakeToken(TokenType.BangEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Bang, start, 1);
        }
    }

    private Token LexLess()
    {
        var start = Position;
        if (TryMatch("<<="))
        {
            return MakeToken(TokenType.LeftShiftEqual, start, 3);
        }
        else if (TryMatch("<<"))
        {
            return MakeToken(TokenType.LeftShift, start, 2);
        }
        else if (TryMatch("<="))
        {
            return MakeToken(TokenType.LessEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Less, start, 1);
        }
    }

    private Token LexGreater()
    {
        var start = Position;
        if (TryMatch(">>="))
        {
            return MakeToken(TokenType.RightShiftEqual, start, 3);
        }
        else if (TryMatch(">="))
        {
            return MakeToken(TokenType.GreaterEqual, start, 2);
        }
        else if (TryMatch(">>"))
        {
            return MakeToken(TokenType.RightShift, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Greater, start, 1);
        }
    }

    private Token LexColon()
    {
        var start = Position;
        if (TryMatch("::="))
        {
            return MakeToken(TokenType.ColonColonEqual, start, 3);
        }
        else if (TryMatch("::"))
        {
            return MakeToken(TokenType.ColonColon, start, 2);
        }
        else if (TryMatch(":="))
        {
            return MakeToken(TokenType.ColonEqual, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Colon, start, 1);
        }
    }

    private Token LexDot()
    {
        var start = Position;
        if (TryMatch(".."))
        {
            return MakeToken(TokenType.DotDot, start, 2);
        }
        else
        {
            Advance();
            return MakeToken(TokenType.Dot, start, 1);
        }
    }

    // private Token LexLeftBracket()
    // {
    //     var start = _position;
    //     if (TryMatch("[]"))
    //     {
    //         return MakeToken(TokenType.Slice, start, 2);
    //     }
    //     else if (_current == '[' && Char.IsDigit(_peek(1)))
    //     {
    //         while(!_isAtEnd && _current != ']')
    //         {
    //             _advance();
    //         }

    //         _advance();
    //         return MakeToken(TokenType.Array, start, _position - start + 1);
    //     }
    //     else if (_current == '[' && (Char.IsLetter(_peek(1)) || _peek(1) == '_'))
    //     {
    //         while(!_isAtEnd && _current != ']')
    //         {
    //             _advance();
    //         }

    //         _advance();
    //         return MakeToken(TokenType.Array, start, _position - start + 1);
    //     }
    //     else
    //     {
    //         _advance();
    //         return MakeToken(TokenType.LeftBracket, start, 1);
    //     }
    // }
}