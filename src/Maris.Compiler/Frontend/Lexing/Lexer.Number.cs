namespace Maris.Compiler.Lexing;

public sealed partial class Lexer
{
    private Token LexNumber()
    {
        var start = Position;
        var baseValue = 10; // Default decimal
        var hasDot = false;
        var hasExponent = false;

        // 1. Handle Prefixes (0x, 0o, 0b)
        if (Current == '0' && Peek(1) != '\0')
        {
            var next = Peek(1);
            if (next is 'x' or 'X') { baseValue = 16; Advance(2); }
            else if (next is 'o' or 'O') { baseValue = 8; Advance(2); }
            else if (next is 'b' or 'B') { baseValue = 2; Advance(2); }
        }

        // 2. Scan Digits, Underscores, Dot, and Exponent
        while (!IsAtEnd)
        {
            if (IsValidDigit(Current, baseValue))
            {
                Advance();
            }
            else if (Current == '_')
            {
                // Validate underscore: must be between digits
                if (!IsValidDigit(Peek(-1), baseValue) || !IsValidDigit(Peek(1), baseValue))
                {
                    // Report Error: Misplaced underscore
                    return MakeToken(TokenType.Invalid, start, Position - start);
                }
                Advance();
            }
            else if (Current == '.' && !hasDot && !hasExponent && baseValue != 16) 
            {
                // Note: Hex floats often use 'p' exponent, dot rules vary by language spec
                // For standard decimal/hex floats:
                if (baseValue == 10 || baseValue == 8) 
                {
                     hasDot = true;
                     Advance();
                }
                else 
                {
                    break; // Dot not allowed in this base or handled differently
                }
            }
            else if ((Current == 'e' || Current == 'E') && !hasExponent && baseValue == 10)
            {
                hasExponent = true;
                Advance();
                // Handle optional sign in exponent
                if (Current is '+' or '-') Advance();
            }
            else if ((Current == 'p' || Current == 'P') && !hasExponent && baseValue == 16)
            {
                // Hex float exponent (e.g., 0x1.5p2)
                hasExponent = true;
                Advance();
                if (Current is '+' or '-') Advance();
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

        return MakeToken(type, start, Position - start);
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