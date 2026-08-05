using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Plus(       
    )
    {
        Equal(
            "+ ++ +=",
            (TokenType.Plus, "+"),
            (TokenType.PlusPlus, "++"),
            (TokenType.PlusEqual, "+="),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Plus_NoWhitespace()
    {
        Equal(
            "++++=",
            (TokenType.PlusPlus, "++"),
            (TokenType.PlusPlus, "++"),
            (TokenType.Equal, "="),
            (TokenType.EOF, "")
        );
    }
}