using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class Character
{
    [Fact]
    public void LexCharacter_ShouldReturnCharacterLiteralToken()
    {
        var text = "'a'";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        IReadOnlyList<Token> tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "'a'"));
        Assert.True(tokens.Contains(TokenKind.CharacterLiteral, TokenKind.Eof));
    }

    [Fact]
    public void LexCharacter_ShouldReturnInvalid()
    {
        var text = "'a";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "'a"));
        Assert.True(tokens.Contains(TokenKind.Invalid, TokenKind.Eof));
    }
}