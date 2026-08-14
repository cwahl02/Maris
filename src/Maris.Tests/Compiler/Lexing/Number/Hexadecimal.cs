using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public partial class LexerTests
{
    [Fact]
    public void LexNumber_Hexadecimal_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0xDEADBEEF");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0xDEADBEEF");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_HexadecimalUppercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0XDEADBEEF");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0XDEADBEEF");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_HexadecimalLowercase_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0xdeadbeef");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0xdeadbeef");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexNumber_HexadecimalWithUnderscores_ShouldReturnIntegerLiteral()
    {
        var lexer = new Lexer("0xDEAD_BEEF");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "0xDEAD_BEEF");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.IntegerLiteral, TokenType.EOF);
    }
}