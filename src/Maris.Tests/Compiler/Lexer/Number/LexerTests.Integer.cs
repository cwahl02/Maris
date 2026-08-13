using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Integer_ShouldReturnIntegerLiteral()
    {
        Equal(
            "123",
            (TokenType.IntegerLiteral, "123"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexNumber_IntegerWithUnderscores_ShouldReturnIntegerLiteral()
    {
        Equal(
            "1_2_3",
            (TokenType.IntegerLiteral, "1_2_3"),
            (TokenType.EOF, "")
        );
    }
}