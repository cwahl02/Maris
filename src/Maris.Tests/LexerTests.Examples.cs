using Xunit;
using Maris.Compiler;

namespace Maris.Tests;

public class LexerTests_Examples
{
    private readonly ITestOutputHelper _output;

    public LexerTests_Examples(ITestOutputHelper output)
    {
        _output = output;
    }


    [Fact]
    public void LexerTests_LexHelloWorld()
    {
        // Arrange
        var source = @"
            import cstd.io as io;

            main :: () -> i32 {
                io.print(""Hello, World!"");
                return 0;
            }
            ";
        // var source = "import cstd as io;";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        var expectedTokens = new List<TokenType>
        {
            TokenType.Import,
            TokenType.Identifier, // cstd
            TokenType.Dot,
            TokenType.Identifier, // io
            TokenType.As,
            TokenType.Identifier, // io
            TokenType.Semicolon,

            TokenType.Identifier, // main
            TokenType.ColonColon,
            TokenType.LeftParen,
            TokenType.RightParen,
            TokenType.Arrow,
            TokenType.I32, // i32
            TokenType.LeftBrace,

            TokenType.Identifier, // io
            TokenType.Dot,
            TokenType.Identifier, // print
            TokenType.LeftParen,
            TokenType.StringLiteral, // "Hello, World!"
            TokenType.RightParen,
            TokenType.Semicolon,

            TokenType.Return,
            TokenType.IntegerLiteral, // 0
            TokenType.Semicolon,

            TokenType.RightBrace,

            TokenType.EndOfFile
        };

        Assert.Equal(expectedTokens.Count, tokens.Count);
        Assert.Equal(expectedTokens, tokens.Select(t => t.Type).ToList());
    }

    [Fact]
    public void LexerTests_LexFunctionWithParameters()
    {
        // Arrange
        var source = @"
            add :: (a: i32, b: i32) -> i32 {
                return a + b;
            }
            ";
        var sourceFile = new SourceFile("test", source);
        var textWindow = new TextWindow(sourceFile.Text);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        var expectedTokens = new List<TokenType>
        {
            TokenType.Identifier, // add
            TokenType.ColonColon,
            TokenType.LeftParen,
            TokenType.Identifier, // a
            TokenType.Colon,
            TokenType.I32, // i32
            TokenType.Comma,
            TokenType.Identifier, // b
            TokenType.Colon,
            TokenType.I32, // i32
            TokenType.RightParen,
            TokenType.Arrow,
            TokenType.I32, // i32
            TokenType.LeftBrace,

            TokenType.Return,
            TokenType.Identifier, // a
            TokenType.Plus,
            TokenType.Identifier, // b
            TokenType.Semicolon,

            TokenType.RightBrace,

            TokenType.EndOfFile
        };

        Assert.Equal(expectedTokens.Count, tokens.Count);
        Assert.Equal(expectedTokens, tokens.Select(t => t.Type).ToList());
    }

    [Fact]
    public void LexerTests_LexFunctionWithParametersAndDefaultValues()
    {
        // Arrange
        var source = @"
            add :: (a: i32 = 0, b: i32 = 0) -> i32 {
                return a + b;
            }
            ";
        var sourceFile = new SourceFile("test", source);
        var textWindow = new TextWindow(sourceFile.Text);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        var expectedTokens = new List<TokenType>
        {
            TokenType.Identifier, // add
            TokenType.ColonColon,
            TokenType.LeftParen,
            TokenType.Identifier, // a
            TokenType.Colon,
            TokenType.I32, // i32
            TokenType.Equal,
            TokenType.IntegerLiteral, // 0
            TokenType.Comma,
            TokenType.Identifier, // b
            TokenType.Colon,
            TokenType.I32, // i32
            TokenType.Equal,
            TokenType.IntegerLiteral, // 0
            TokenType.RightParen,
            TokenType.Arrow,
            TokenType.I32, // i32
            TokenType.LeftBrace,

            TokenType.Return,
            TokenType.Identifier, // a
            TokenType.Plus,
            TokenType.Identifier, // b
            TokenType.Semicolon,

            TokenType.RightBrace,

            TokenType.EndOfFile
        };

        Assert.Equal(expectedTokens.Count, tokens.Count);
        Assert.Equal(expectedTokens, tokens.Select(t => t.Type).ToList());
    }
    
    [Fact]
    public void LexerTests_LexEnumDefinition()
    {
        // Arrange
        var source = @"
            Color :: enum {
                Red,
                Green,
                Blue
            }
            ";
        var sourceFile = new SourceFile("test", source);
        var textWindow = new TextWindow(sourceFile.Text);
        var lexer = new Lexer(sourceFile);

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        var expectedTokens = new List<TokenType>
        {
            TokenType.Identifier, // Color
            TokenType.ColonColon, // ::
            TokenType.Enum,
            TokenType.LeftBrace,

            TokenType.Identifier, // Red
            TokenType.Comma,
            TokenType.Identifier, // Green
            TokenType.Comma,
            TokenType.Identifier, // Blue

            TokenType.RightBrace,

            TokenType.EndOfFile
        };

        Assert.Equal(expectedTokens.Count, tokens.Count);
        Assert.Equal(expectedTokens, tokens.Select(t => t.Type).ToList());
    }
}