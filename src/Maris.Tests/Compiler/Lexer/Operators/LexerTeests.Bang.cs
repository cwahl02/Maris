using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Bang_ShouldReturnValid(       
    )
    {
        Equal(
            "! !=",
            (TokenType.Bang, "!"),
            (TokenType.BangEqual, "!="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Bang_NoWhitespace_ShouldReturnValid()
    {
        Equal(
            "!!=!!",
            (TokenType.Bang, "!"),
            (TokenType.BangEqual, "!="),
            (TokenType.Bang, "!"),
            (TokenType.Bang, "!"),
            (TokenType.EOF, "")
        );
    }
}