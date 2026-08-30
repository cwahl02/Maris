using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Percent
{
    [Fact]
    public void Lex_Percent()
    {
        var text = "% %=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "%", "%="));
        Assert.True(tokens.Contains(SyntaxTokenKind.Percent, SyntaxTokenKind.PercentEqual, SyntaxTokenKind.Eof));
    }

    [Fact]
    public void Lex_Percent_NoWhitespace()
    {
        var text = "%%=%";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "%", "%=", "%"));
        Assert.True(tokens.Contains(SyntaxTokenKind.Percent, SyntaxTokenKind.PercentEqual, SyntaxTokenKind.Percent, SyntaxTokenKind.Eof));
    }
}