using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Minus(       
    )
    {
        Equal(
            "- -- -=",
            (TokenType.Minus, "-"),
            (TokenType.MinusMinus, "--"),
            (TokenType.MinusEqual, "-="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Minus_NoWhitespace()
    {
        Equal(
            "----=",
            (TokenType.MinusMinus, "--"),
            (TokenType.MinusMinus, "--"),
            (TokenType.Equal, "="),
            (TokenType.EOF, "")
        );
    }
}