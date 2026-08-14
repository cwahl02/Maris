using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Integer_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("123");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "123");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_IntegerWithUnderscores_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("1_2_3");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "1_2_3");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }
}