using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Octal_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0o123");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0o123");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_OctalUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0O123");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0O123");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_OctalWithUnderscores_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0o123_456");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0o123_456");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_OctalWithUnderscoresUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0O123_456");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0O123_456");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }
}