namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexNumber()
    {
        var start = _iterator.Position;
        var baseValue = 10; // Default decimal
        var hasDot = false;
        var hasExponent = false;

        // 1. Handle Prefixes (0x, 0o, 0b)
        if (_iterator.Current == '0' && _iterator.Peek(1) != '\0')
        {
            var next = _iterator.Peek(1);
            if (next is 'x' or 'X') { baseValue = 16; _iterator.Forward(2); }
            else if (next is 'o' or 'O') { baseValue = 8; _iterator.Forward(2); }
            else if (next is 'b' or 'B') { baseValue = 2; _iterator.Forward(2); }
        }

        // 2. Scan Digits, Underscores, Dot, and Exponent
        while (!_iterator.IsAtEnd)
        {
            if (IsValidDigit(_iterator.Current, baseValue))
            {
                _iterator.Forward();
            }
            else if (_iterator.Current == '_')
            {
                // Validate underscore: must be between digits
                if (!IsValidDigit(_iterator.Peek(-1), baseValue) || !IsValidDigit(_iterator.Peek(1), baseValue))
                {
                    // Report Error: Misplaced underscore
                    return new Token(TokenKind.Invalid, start, _iterator.Position - start);
                }
                _iterator.Forward();
            }
            else if (_iterator.Current == '.' && !hasDot && !hasExponent && baseValue != 16) 
            {
                // Note: Hex floats often use 'p' exponent, dot rules vary by language spec
                // For standard decimal/hex floats:
                if (baseValue == 10 || baseValue == 8) 
                {
                     hasDot = true;
                     _iterator.Forward();
                }
                else 
                {
                    break; // Dot not allowed in this base or handled differently
                }
            }
            else if ((_iterator.Current == 'e' || _iterator.Current == 'E') && !hasExponent && baseValue == 10)
            {
                hasExponent = true;
                _iterator.Forward();
                // Handle optional sign in exponent
                if (_iterator.Current is '+' or '-') _iterator.Forward();
            }
            else if ((_iterator.Current == 'p' || _iterator.Current == 'P') && !hasExponent && baseValue == 16)
            {
                // Hex float exponent (e.g., 0x1.5p2)
                hasExponent = true;
                _iterator.Forward();
                if (_iterator.Current is '+' or '-') _iterator.Forward();
            }
            else
            {
                break; // End of number
            }
        }

        // 3. Determine Token Type
        var type = (hasDot || hasExponent) ? TokenKind.FloatLiteral : TokenKind.IntegerLiteral;
        
        // Optional: Handle Suffixes (e.g., f, L, u) here if needed
        // ScanSuffix(); 

        return new Token(type, start, _iterator.Position - start);
    }

    private bool IsValidDigit(char c, int baseValue)
    {
        if (char.IsDigit(c))
        {
            var val = c - '0';
            return val < baseValue;
        }
        
        if (baseValue > 10)
        {
            if (c >= 'a' && c <= 'f') return (c - 'a' + 10) < baseValue;
            if (c >= 'A' && c <= 'F') return (c - 'A' + 10) < baseValue;
        }
        
        return false;
    }
}