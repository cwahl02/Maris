using Maris.Compiler.Text;

namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private enum NumberBase
    {
        Decimal,
        Binary,
        Octal,
        Hexadecimal
    }

    private Token LexNumber()
    {
        int start = _window.Position;

        NumberBase numberBase = NumberBase.Decimal;
        bool isFloat = false;

        ScanBasePrefix(ref numberBase);

        ScanDigits(numberBase);

        if (ScanFraction(numberBase))
            isFloat = true;

        if (ScanExponent(numberBase))
            isFloat = true;

        ScanSuffix(ref isFloat);

        return MakeToken(
            isFloat ? TokenType.FloatLiteral : TokenType.IntegerLiteral,
            start,
            _window.Position - start);
    }

    private void ScanBasePrefix(ref NumberBase numberBase)
    {
        if (_window.Current != '0')
            return;

        switch (_window.Peek(1))
        {
            case 'x':
            case 'X':
                numberBase = NumberBase.Hexadecimal;
                _window.Advance(2);
                break;

            case 'b':
            case 'B':
                numberBase = NumberBase.Binary;
                _window.Advance(2);
                break;

            case 'o':
            case 'O':
                numberBase = NumberBase.Octal;
                _window.Advance(2);
                break;
        }
    }

    private void ScanDigits(NumberBase numberBase)
    {
        while (true)
        {
            if (_window.Current == '_')
            {
                _window.Advance();
                continue;
            }

            if (!IsDigit(_window.Current, numberBase))
                break;

            _window.Advance();
        }
    }

    private bool ScanFraction(NumberBase numberBase)
    {
        if (_window.Current != '.')
            return false;
        
        if (_window.Peek(1) == '.')
            return false; // Prevents confusion with the range operator '..'

        if (numberBase != NumberBase.Decimal)
            return false;

        _window.Advance();

        ScanDigits(numberBase);

        return true;
    }

    private bool ScanExponent(NumberBase numberBase)
    {
        bool validExponent = numberBase switch
        {
            NumberBase.Hexadecimal =>
                _window.Current is 'p' or 'P',

            _ =>
                _window.Current is 'e' or 'E'
        };

        if (!validExponent)
            return false;

        _window.Advance();

        if (_window.Current is '+' or '-')
            _window.Advance();

        ScanDigits(NumberBase.Decimal);

        return true;
    }

    private void ScanSuffix(ref bool isFloat)
    {
        switch (_window.Current)
        {
            case 'f':
            case 'F':
            case 'd':
            case 'D':
                isFloat = true;
                _window.Advance();
                break;

            case 'u':
            case 'U':
            case 'l':
            case 'L':
                _window.Advance();

                if (_window.Current is 'u' or 'U' or 'l' or 'L')
                    _window.Advance();

                break;
        }
    }

    private static bool IsDigit(char c, NumberBase numberBase)
    {
        return numberBase switch
        {
            NumberBase.Binary =>
                c is '0' or '1',

            NumberBase.Octal =>
                c >= '0' && c <= '7',

            NumberBase.Decimal =>
                char.IsDigit(c),

            NumberBase.Hexadecimal =>
                char.IsDigit(c) ||
                (c >= 'a' && c <= 'f') ||
                (c >= 'A' && c <= 'F'),

            _ => false
        };
    }
}
