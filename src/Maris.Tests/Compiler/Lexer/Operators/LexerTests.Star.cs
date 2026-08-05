using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Star(       
    )
    {
        Equal(
            "* *=",
            (TokenType.Star, "*"),
            (TokenType.StarEqual, "*="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Star_NoWhitespace()
    {
        Equal(
            "**=*",
            (TokenType.Star, "*"),
            (TokenType.StarEqual, "*="),
            (TokenType.Star, "*"),
            (TokenType.EOF, "")
        );
    }
}