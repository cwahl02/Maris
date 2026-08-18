using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Ampersand
{
    [Fact]
    public void Lex_Ampersand()
    {
        var text = "& && &=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "&", "&&", "&="));
        Assert.True(tokens.Contains(TokenKind.Ampersand, TokenKind.AmpersandAmpersand, TokenKind.AmpersandEqual, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Ampersand_NoWhitespace()
    {
        var text = "&&&=&";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "&&", "&=", "&"));
        Assert.True(tokens.Contains(TokenKind.AmpersandAmpersand, TokenKind.AmpersandEqual, TokenKind.Ampersand, TokenKind.Eof));
    }
}