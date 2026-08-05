using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Slash(       
    )
    {
        Equal(
            "/ /=",
            (TokenType.Slash, "/"),
            (TokenType.SlashEqual, "/="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Slash_NoWhitespace()
    {
        Equal(
            "/=/",
            (TokenType.SlashEqual, "/="),
            (TokenType.Slash, "/"),
            (TokenType.EOF, "")
        );
    }
}