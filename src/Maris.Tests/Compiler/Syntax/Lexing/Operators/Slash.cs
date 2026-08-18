using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Slash
{
    [Fact]
    public void Lex_Slash()
    {
        var text = "/ /=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "/", "/="));
        Assert.True(tokens.Contains(TokenKind.Slash, TokenKind.SlashEqual, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Slash_NoWhitespace()
    {
        var text = "/=/";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "/=", "/"));
        Assert.True(tokens.Contains(TokenKind.SlashEqual, TokenKind.Slash, TokenKind.Eof));
    }
}