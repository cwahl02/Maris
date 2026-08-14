using Maris.Compiler.Lexer;
using Xunit;

public class Minus
{
    [Fact]
    public void Lex_Minus(       
    )
    {
        var lexer = new Lexer("- -- -=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "-", "--", "-=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Minus, TokenType.MinusMinus, TokenType.MinusEqual, TokenType.EOF);
    }

    [Fact]
    public void Lex_Minus_NoWhitespace()
    {
        var lexer = new Lexer("----=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "--", "--", "=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.MinusMinus, TokenType.MinusMinus, TokenType.Equal, TokenType.EOF);
    }
}