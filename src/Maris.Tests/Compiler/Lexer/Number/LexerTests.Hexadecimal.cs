using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Hexadecimal_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0xDEADBEEF",
            (TokenType.IntegerLiteral, "0xDEADBEEF"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_HexadecimalUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0XDEADBEEF",
            (TokenType.IntegerLiteral, "0XDEADBEEF"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_HexadecimalLowercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0xdeadbeef",
            (TokenType.IntegerLiteral, "0xdeadbeef"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_HexadecimalWithUnderscores_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0xDEAD_BEEF",
            (TokenType.IntegerLiteral, "0xDEAD_BEEF"),
            (TokenType.EOF, "")
        );
    }
}