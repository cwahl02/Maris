using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Binary
{
    [Fact]
    public void LexNumber_Binary_ShouldReturnIntegerLiteral()
    {
        var text = "0b1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryUppercase_ShouldReturnIntegerLiteral()
    {
        var text = "0B1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithUnderscores_ShouldReturnIntegerLiteral()
    {
        var text = "0b1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        var text = "0B1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithMultipleUnderscores_ShouldReturnIntegerLiteral()
    {
        var text = "0b1010_1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b1010_1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithMultipleUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        var text = "0B1010_1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B1010_1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithLeadingZeros_ShouldReturnIntegerLiteral()
    {
        var text = "0b00001010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b00001010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithLeadingZerosUppercase_ShouldReturnIntegerLiteral()
    {
        var text = "0B00001010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B00001010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithTrailingZeros_ShouldReturnIntegerLiteral()
    {
        var text = "0b10100000";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b10100000"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexNumber_BinaryWithTrailingZerosUppercase_ShouldReturnIntegerLiteral()
    {
        var text = "0B10100000";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B10100000"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }
}