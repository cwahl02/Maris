using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public class Percent
{
    [Fact]
    public void Lex_Percent()
    {
        var lexer = new Lexer("% %=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "%", "%=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Percent, TokenType.PercentEqual, TokenType.EOF);
    }

    [Fact]
    public void Lex_Percent_NoWhitespace()
    {
        var lexer = new Lexer("%%=%");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "%", "%=", "%");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Percent, TokenType.PercentEqual, TokenType.Percent, TokenType.EOF);
    }
}