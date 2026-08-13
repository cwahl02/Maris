using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Octal_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0o123",
            (TokenType.IntegerLiteral, "0o123"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_OctalUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0O123",
            (TokenType.IntegerLiteral, "0O123"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_OctalWithUnderscores_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0o123_456",
            (TokenType.IntegerLiteral, "0o123_456"),
            (TokenType.EOF, "")
            );
    }

    [Fact]
    public void LexNumber_OctalWithUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        Equal(
            "0O123_456",
            (TokenType.IntegerLiteral, "0O123_456"),
            (TokenType.EOF, "")
        );
    }
}