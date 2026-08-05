using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Ampersand(       
    )
    {
        Equal(
            "& && &=",
            (TokenType.Ampersand, "&"),
            (TokenType.AmpersandAmpersand, "&&"),
            (TokenType.AmpersandEqual, "&="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Ampersand_NoWhitespace()
    {
        Equal(
            "&&&=&",
            (TokenType.AmpersandAmpersand, "&&"),
            (TokenType.AmpersandEqual, "&="),
            (TokenType.Ampersand, "&"),
            (TokenType.EOF, "")
        );
    }
}