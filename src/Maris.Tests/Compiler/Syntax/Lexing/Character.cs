using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class Character
{
    [Fact]
    public void Lex_Character()
    {
        var text = "'a'";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        IReadOnlyList<SyntaxToken> tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "'a'"));
        Assert.True(tokens.Contains(SyntaxTokenKind.CharacterLiteral, SyntaxTokenKind.Eof));
    }

    [Fact]
    public void Lex_Character_MissingEndQuote()
    {
        var text = "'a";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "'a"));
        Assert.True(tokens.Contains(SyntaxTokenKind.Invalid, SyntaxTokenKind.Eof));
    }
}