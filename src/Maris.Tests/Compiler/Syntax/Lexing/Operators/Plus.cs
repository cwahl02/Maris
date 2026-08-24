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
        Assert.True(tokens.Contains(SyntaxTokenKind.Plus, SyntaxTokenKind.PlusPlus, SyntaxTokenKind.PlusEqual, SyntaxTokenKind.Eof));
    }

    [Fact]
    public void Lex_Plus_NoWhitespace()
    {
        var text = "++++=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "++", "++", "="));
        Assert.True(tokens.Contains(SyntaxTokenKind.PlusPlus, SyntaxTokenKind.PlusPlus, SyntaxTokenKind.Equal, SyntaxTokenKind.Eof));
    }
}