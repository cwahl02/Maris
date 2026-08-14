using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public class Bang
{
    [Fact]
    public void Lex_Bang_ShouldReturnValid()
    {
        var lexer = new Lexer("! !=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "!", "!=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Bang, TokenType.BangEqual, TokenType.EOF);
    }

    [Fact]
    public void Lex_Bang_NoWhitespace_ShouldReturnValid()
    {
        var lexer = new Lexer("! !=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "!", "!=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Bang, TokenType.BangEqual, TokenType.EOF);
    }
}