namespace Maris.Compiler.Lexing;

using Maris.Core.Text;

public readonly struct Token(TokenType type, int start, int length, string text) : IEquatable<Token>
{
    public TokenType Type { get; } = type;
    public string Text { get; } = text;
    public int Start { get; } = start;
    public int Length { get; } = length;
    public static Token Eof => new(TokenType.EOF, -1, 0, string.Empty);

    public override string ToString()
    {
        // Prints cleanly in xUnit error output: "Identifier: 'foo' [0..3]"
        return $"{Type}: '{Text}' [{Start}..{Start + Length}]";
    }


    // 1. Core IEquatable implementation
    public bool Equals(Token other)
    {
        return Type == other.Type &&
               Start == other.Start &&
               Length == other.Length;
    }

    // 2. Override base Object.Equals
    public override bool Equals(object? obj)
    {
        return obj is Token other && Equals(other);
    }

    // 3. Override GetHashCode (highly recommended when overriding Equals)
    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Start, Length);
    }

    // 4. Implement standard equality operators
    public static bool operator ==(Token left, Token right) => left.Equals(right);
    public static bool operator !=(Token left, Token right) => !left.Equals(right);
}