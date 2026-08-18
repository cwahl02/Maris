using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Binary
{
    [Fact]
    public void Lex_Binary()
    {
        var text = "0b1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_Uppercase()
    {
        var text = "0B1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithUnderscores()
    {
        var text = "0b1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithUnderscoresUppercase()
    {
        var text = "0B1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithMultipleUnderscores()
    {
        var text = "0b1010_1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b1010_1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithMultipleUnderscores_Uppercase()
    {
        var text = "0B1010_1010_1010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B1010_1010_1010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithLeadingZeros()
    {
        var text = "0b00001010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b00001010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithLeadingZeros_Uppercase()
    {
        var text = "0B00001010";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B00001010"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithTrailingZeros()
    {
        var text = "0b10100000";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0b10100000"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Binary_WithTrailingZeros_Uppercase()
    {
        var text = "0B10100000";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "0B10100000"));
        Assert.True(tokens.Contains(TokenKind.IntegerLiteral, TokenKind.Eof));
    }
}