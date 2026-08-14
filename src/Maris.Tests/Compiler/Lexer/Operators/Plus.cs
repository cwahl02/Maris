using Maris.Compiler.Lexer;
using Xunit;

public class Plus
{
    [Fact]
    public void Lex_Plus(       
    )
    {
        var lexer = new Lexer("+ ++ +=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "+", "++", "+=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Plus, TokenType.PlusPlus, TokenType.PlusEqual, TokenType.EOF);
    }

    [Fact]
    public void Lex_Plus_NoWhitespace()
    {
        var lexer = new Lexer("++++=");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "++", "++", "=");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.PlusPlus, TokenType.PlusPlus, TokenType.Equal, TokenType.EOF);
    }
}