using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class Bang
{
    [Fact]
    public void Lex_Bang()
    {
        var text = "! !=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "!", "!="));
        Assert.True(tokens.Contains(SyntaxTokenKind.Bang, SyntaxTokenKind.BangEqual, SyntaxTokenKind.Eof));
    }

    [Fact]
    public void Lex_Bang_NoWhitespace()
    {
        var text = "!!=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "!", "!="));
        Assert.True(tokens.Contains(SyntaxTokenKind.Bang, SyntaxTokenKind.BangEqual, SyntaxTokenKind.Eof));
    }
}