using Maris.Compiler.Lexing;
using Maris.Compiler.Text;
using Xunit;

namespace Maris.Tests;

public class LexerTests_Numbers
{
    [Fact]
    public void LexerTests_LexIntegerLiteral()
    {
        // Arrange
        var source = "12345";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.IntegerLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 5), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatLiteral()
    {
        // Arrange
        var source = "3.14159";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 7), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexHexadecimalLiteral()
    {
        // Arrange
        var source = "0x1A3F";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.IntegerLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 6), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexBinaryLiteral()
    {
        // Arrange
        var source = "0b1010";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.IntegerLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 6), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexOctalLiteral()
    {
        // Arrange
        var source = "0o755";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.IntegerLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 5), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithExponent()
    {
        // Arrange
        var source = "1.23e4";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 6), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithNegativeExponent()
    {
        // Arrange
        var source = "1.23e-4";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 7), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithPositiveExponent()
    {
        // Arrange
        var source = "1.23e+4";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 7), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithoutLeadingDigit()
    {
        // Arrange
        var source = ".123";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 4), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithoutTrailingDigit()
    {
        // Arrange
        var source = "123.";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 4), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithExponentWithoutLeadingDigit()
    {
        // Arrange
        var source = ".123e4";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 6), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexFloatWithExponentWithoutTrailingDigit()
    {
        // Arrange
        var source = "123.e4";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.FloatLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 6), tokens[0].Span);
    }
}