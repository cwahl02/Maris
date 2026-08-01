namespace Maris.Tests;

using Maris.Compiler;
using Xunit;

public class LexerTests
{
    private readonly ITestOutputHelper _output;

    public LexerTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void LexerTests_LexIdentifier()
    {
        // Arrange
        var source = "abc";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.Identifier, tokens[0].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[1].Type);
        Assert.Equal(new TextSpan(0, 3), tokens[0].Span);
    }
    
    [Fact]
    public void LexerTests_LexReservedSymbols()
    {
        // Arrange
        var source = "if else while for return";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(6, tokens.Count);
        Assert.Equal(TokenType.If, tokens[0].Type);
        Assert.Equal(TokenType.Else, tokens[1].Type);
        Assert.Equal(TokenType.While, tokens[2].Type);
        Assert.Equal(TokenType.For, tokens[3].Type);
        Assert.Equal(TokenType.Return, tokens[4].Type);
        Assert.Equal(TokenType.EndOfFile, tokens[5].Type);
    }

    [Fact]
    public void LexerTests_LexComments()
    {
        // Arrange
        var source = @"
            // This is a line comment
            if (true) {
                /* This is a block comment */
                return 42;
            }
        ";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);
        var expectedTokens = new List<TokenType>
        {
            TokenType.If,
            TokenType.LeftParen,
            TokenType.Identifier, // Assuming 'true' is treated as an identifier
            TokenType.RightParen,
            TokenType.LeftBrace,
            TokenType.Return,
            TokenType.IntegerLiteral, // Assuming '42' is treated as an integer literal
            TokenType.Semicolon,
            TokenType.RightBrace,
            TokenType.EndOfFile
        };

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(expectedTokens.Count, tokens.Count);
        for (int i = 0; i < tokens.Count; i++)
        {
            Assert.Equal(expectedTokens[i], tokens[i].Type);
        }
    }

    [Fact]
    public void LexerTests_LexStringLiteral()
    {
        // Arrange
        var source = "\"Hello, world!\"";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);
        var expectedTokens = new List<TokenType>
        {
            TokenType.StringLiteral,
            TokenType.EndOfFile
        };

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(2, tokens.Count);
        for (int i = 0; i < tokens.Count; i++)
        {
            Assert.Equal(expectedTokens[i], tokens[i].Type);
        }
    }

    [Fact]
    public void LexerTests_LexIdentiferWithAccessor()
    {
        // Arrange
        var source = "import cstd.io as io;";
        var sourceFile = new SourceFile("test", source);

        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();
        var expectedTokens = new List<TokenType>
        {
            TokenType.Import,
            TokenType.Identifier, // cstd
            TokenType.Dot,
            TokenType.Identifier, // io
            TokenType.As,
            TokenType.Identifier, // io
            TokenType.Semicolon,
            TokenType.EndOfFile
        };

        // Assert
        Assert.Equal(expectedTokens.Count, tokens.Count);
        for (int i = 0; i < tokens.Count; i++)
        {
            Assert.Equal(expectedTokens[i], tokens[i].Type);
        }
    }
}
