using Maris.Compiler.Lexer;
using Xunit;

public class Binary
{
    [Fact]
    public void LexNumber_Binary_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0b1010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0b1010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0B1010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0B1010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithUnderscores_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0b1010_1010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0b1010_1010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0B1010_1010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0B1010_1010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithMultipleUnderscores_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0b1010_1010_1010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0b1010_1010_1010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithMultipleUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0B1010_1010_1010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0B1010_1010_1010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithLeadingZeros_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0b00001010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0b00001010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithLeadingZerosUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0B00001010");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0B00001010");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithTrailingZeros_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0b10100000");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0b10100000");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_BinaryWithTrailingZerosUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0B10100000");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0B10100000");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }
}