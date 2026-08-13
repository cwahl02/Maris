using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexString_ShouldReturnStringToken()
    {
        Equal(
            "\"Hello, World!\"",
            (TokenType.StringLiteral, "\"Hello, World!\""),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexString_ShouldReturnInvalid()
    {
        Equal(
            "\"Hello, World!",
            (TokenType.Invalid, "\"Hello, World!"),
            (TokenType.EOF, "")
        );
    }
}