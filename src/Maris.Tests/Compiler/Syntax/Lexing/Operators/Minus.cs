using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class Minus
{
    [Fact]
    public void Lex_Minus()
    {
        var text = "- -- -=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "-", "--", "-="));
        Assert.True(tokens.Contains(SyntaxTokenKind.Minus, SyntaxTokenKind.MinusMinus, SyntaxTokenKind.MinusEqual, SyntaxTokenKind.Eof));
    }

    [Fact]
    public void Lex_Minus_NoWhitespace()
    {
        var text = "----=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "--", "--", "="));
        Assert.True(tokens.Contains(SyntaxTokenKind.MinusMinus, SyntaxTokenKind.MinusMinus, SyntaxTokenKind.Equal, SyntaxTokenKind.Eof));
    }
}