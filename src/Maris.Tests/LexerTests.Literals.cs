using Xunit;
using Maris.Compiler;

namespace Maris.Tests;

public class LexerTests_Literals
{
    [Fact]
    public void LexerTests_LexStringLiteral_ShouldPass()
    {
        // Arrange
        var source = "\"Hello, World!\"";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.StringLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 15), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexStringLiteralWithEscapeSequences_ShouldPass()
    {
        // Arrange
        var source = "\"Hello, \\\"World\\\"!\"";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.StringLiteral, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 19), tokens[0].Span);
    }

    [Fact]
    public void LexerTests_LexStringLiteralWithNewline_ShouldPass()
    {
        // Arrange
        var source = """
                "Hello,
                World!"
                """;
        var sourceFile = new SourceFile("test", source);
        var textWindow = new TextWindow(sourceFile.Text);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();
        var actualString = textWindow.Slice(tokens[0].Span.Start, tokens[0].Span.Length).ToString();
        var expectedString = """
                "Hello,
                World!"
                """;

        // Assert
        // Assert.Equal(2, tokens.Count);
        // Assert.Equal(TokenType.StringLiteral, tokens[0].Type);
        // Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        // Assert.Equal(0, tokens[0].Span.Start);
        // Assert.Equal(17, tokens[0].Span.Length);
        Assert.Equal(expectedString, actualString);
        
    }
}