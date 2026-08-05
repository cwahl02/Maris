using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Percent(       
    )
    {
        Equal(
            "% %=",
            (TokenType.Percent, "%"),
            (TokenType.PercentEqual, "%="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Percent_NoWhitespace()
    {
        Equal(
            "%%=%",
            (TokenType.Percent, "%"),
            (TokenType.PercentEqual, "%="),
            (TokenType.Percent, "%"),
            (TokenType.EOF, "")
        );
    }
}