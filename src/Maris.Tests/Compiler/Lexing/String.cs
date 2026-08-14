using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public class String
{
    [Fact]
    public void LexString_ShouldReturnStringToken()
    {
        var lexer = new Lexer("\"Hello, World!\"");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "\"Hello, World!\"");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.StringLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexString_ShouldReturnInvalid()
    {
        var lexer = new Lexer("\"Hello, World!");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "\"Hello, World!");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Invalid, TokenType.EOF);
    }
}