namespace Maris.Compiler.Lexer;

public sealed partial class Lexer
{
    private Token LexNumber()
    {
        var start = _position;
        var baseValue = 10; // Default decimal
        var hasDot = false;
        var hasExponent = false;

        // 1. Handle Prefixes (0x, 0o, 0b)
        if (_current == '0' && _peek(1) != '\0')
        {
            var next = _peek(1);
            if (next is 'x' or 'X') { baseValue = 16; _advance(2); }
            else if (next is 'o' or 'O') { baseValue = 8; _advance(2); }
            else if (next is 'b' or 'B') { baseValue = 2; _advance(2); }
        }

        // 2. Scan Digits, Underscores, Dot, and Exponent
        while (!_isAtEnd)
        {
            if (IsValidDigit(_current, baseValue))
            {
                _advance();
            }
            else if (_current == '_')
            {
                // Validate underscore: must be between digits
                if (!IsValidDigit(_peek(-1), baseValue) || !IsValidDigit(_peek(1), baseValue))
                {
                    // Report Error: Misplaced underscore
                    return MakeToken(TokenType.Invalid, start, _position - start);
                }
                _advance();
            }
            else if (_current == '.' && !hasDot && !hasExponent && baseValue != 16) 
            {
                // Note: Hex floats often use 'p' exponent, dot rules vary by language spec
                // For standard decimal/hex floats:
                if (baseValue == 10 || baseValue == 8) 
                {
                     hasDot = true;
                     _advance();
                }
                else 
                {
                    break; // Dot not allowed in this base or handled differently
                }
            }
            else if ((_current == 'e' || _current == 'E') && !hasExponent && baseValue == 10)
            {
                hasExponent = true;
                _advance();
                // Handle optional sign in exponent
                if (_current is '+' or '-') _advance();
            }
            else if ((_current == 'p' || _current == 'P') && !hasExponent && baseValue == 16)
            {
                // Hex float exponent (e.g., 0x1.5p2)
                hasExponent = true;
                _advance();
                if (_current is '+' or '-') _advance();
            }
            else
            {
                break; // End of number
            }
        }

        // 3. Determine Token Type
        var type = (hasDot || hasExponent) ? TokenType.FloatLiteral : TokenType.IntegerLiteral;
        
        // Optional: Handle Suffixes (e.g., f, L, u) here if needed
        // ScanSuffix(); 

        return MakeToken(type, start, _position - start);
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