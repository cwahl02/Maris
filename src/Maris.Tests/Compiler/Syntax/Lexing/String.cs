using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;
namespace Maris.Tests.Compiler.Syntax.Lexing;

public class String
{
    [Fact]
    public void Lex_String()
    {
        var text = "\"Hello, World!\"";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "\"Hello, World!\""));
        Assert.True(tokens.Contains(TokenKind.StringLiteral, TokenKind.Eof));
    }

    [Fact]
    public void Lex_String_MissingEndQuote()
    {
        var text = "\"Hello, World!";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "\"Hello, World!"));
        Assert.True(tokens.Contains(TokenKind.Invalid, TokenKind.Eof));
    }
}