using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Binary_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0b1010",
            (TokenType.IntegerLiteral, "0b1010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0B1010",
            (TokenType.IntegerLiteral, "0B1010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithUnderscores_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0b1010_1010",
            (TokenType.IntegerLiteral, "0b1010_1010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0B1010_1010",
            (TokenType.IntegerLiteral, "0B1010_1010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithMultipleUnderscores_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0b1010_1010_1010",
            (TokenType.IntegerLiteral, "0b1010_1010_1010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithMultipleUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0B1010_1010_1010",
            (TokenType.IntegerLiteral, "0B1010_1010_1010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithLeadingZeros_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0b00001010",
            (TokenType.IntegerLiteral, "0b00001010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithLeadingZerosUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0B00001010",
            (TokenType.IntegerLiteral, "0B00001010"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithTrailingZeros_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0b10100000",
            (TokenType.IntegerLiteral, "0b10100000"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_BinaryWithTrailingZerosUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0B10100000",
            (TokenType.IntegerLiteral, "0B10100000"),
            (TokenType.EOF, "")
        );
    }
}