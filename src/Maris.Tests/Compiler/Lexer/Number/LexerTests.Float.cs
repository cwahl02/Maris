using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_ShouldReturnFloatLiteral()
    {
        Equal(
            "123.0",
            (TokenType.FloatLiteral, "123.0"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatStartingWithDot_ShouldReturnFloatLiteral()
    {
        Equal(
            ".123",
            (TokenType.FloatLiteral, ".123"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithExponent_ShouldReturnFloatLiteral()
    {
        Equal(
            "1.23e4",
            (TokenType.FloatLiteral, "1.23e4"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithExponentUppercase_ShouldReturnFloatLiteral()
    {
        Equal(
            "1.23E4",
            (TokenType.FloatLiteral, "1.23E4"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithExponentAndSign_ShouldReturnFloatLiteral()
    {
        Equal(
            "1.23e-4",
            (TokenType.FloatLiteral, "1.23e-4"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithExponentAndPositiveSign_ShouldReturnFloatLiteral()
    {
        Equal(
            "1.23e+4",
            (TokenType.FloatLiteral, "1.23e+4"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithLeadingAndTrailingDot_ShouldReturnFloatLiteral()
    {
        Equal(
            "123.",
            (TokenType.FloatLiteral, "123."),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithLeadingDot_ShouldReturnFloatLiteral()
    {
        Equal(
            ".123",
            (TokenType.FloatLiteral, ".123"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithLeadingDotAndExponent_ShouldReturnFloatLiteral()
    {
        Equal(
            ".123e4",
            (TokenType.FloatLiteral, ".123e4"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_FloatWithLeadingDotAndExponentAndSign_ShouldReturnFloatLiteral()
    {
        Equal(
            ".123e-4",
            (TokenType.FloatLiteral, ".123e-4"),
            (TokenType.EOF, "")
        );
    }
}