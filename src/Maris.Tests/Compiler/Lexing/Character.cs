using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Lexing;

public class Character
{
    [Fact]
    public void LexCharacter_ShouldReturnCharacterLiteralToken()
    {
        var sourceFile = new SourceFile("", "'a'");
        var lexer = new Lexer(sourceFile);
        IReadOnlyList<Token> tokens = lexer.Lex();

        Assert.True(tokens.Contains("'a'"));
        Assert.True(tokens.Contains(TokenKind.CharacterLiteral, TokenKind.Eof));
    }

    // [Fact]
    // public void LexCharacter_ShouldReturnInvalid()
    // {
    //     var lexer = new Lexer("'a");
    //     var tokens = lexer.Lex();

    //     LexerAssert.ContainsText(tokens, "'a");
    //     LexerAssert.ContainsTokenTypes(tokens, TokenType.Invalid, TokenType.EOF);
    // }
}