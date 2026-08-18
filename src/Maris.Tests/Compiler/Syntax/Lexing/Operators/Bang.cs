using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class Bang
{
    [Fact]
    public void Lex_Bang_ShouldReturnValid()
    {
        var text = "! !=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "!", "!="));
        Assert.True(tokens.Contains(TokenKind.Bang, TokenKind.BangEqual, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Bang_NoWhitespace_ShouldReturnValid()
    {
        var text = "!!=";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "!", "!="));
        Assert.True(tokens.Contains(TokenKind.Bang, TokenKind.BangEqual, TokenKind.Eof));
    }
}