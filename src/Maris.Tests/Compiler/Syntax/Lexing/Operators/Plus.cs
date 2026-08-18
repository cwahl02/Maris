using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Plus
{
    [Fact]
    public void Lex_Plus()
    {
        var text = "+ ++ +=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "+", "++", "+="));
        Assert.True(tokens.Contains(TokenKind.Plus, TokenKind.PlusPlus, TokenKind.PlusEqual, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Plus_NoWhitespace()
    {
        var text = "++++=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "++", "++", "="));
        Assert.True(tokens.Contains(TokenKind.PlusPlus, TokenKind.PlusPlus, TokenKind.Equal, TokenKind.Eof));
    }
}