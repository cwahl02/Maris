using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;


public class Star
{
    [Fact]
    public void Lex_Star()
    {
        var text = "* *=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "*", "*="));
        Assert.True(tokens.Contains(SyntaxTokenKind.Star, SyntaxTokenKind.StarEqual, SyntaxTokenKind.Eof)); 
    }

    [Fact]
    public void Lex_Star_NoWhitespace()
    {
        var text = "**=*";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "*", "*=", "*"));
        Assert.True(tokens.Contains(SyntaxTokenKind.Star, SyntaxTokenKind.StarEqual, SyntaxTokenKind.Star, SyntaxTokenKind.Eof));
    }
}