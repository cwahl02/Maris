using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public class Character
{
    [Fact]
    public void LexCharacter_ShouldReturnCharacterLiteralToken()
    {
        var lexer = new Lexer("'a'");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "'a'");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.CharacterLiteral, TokenType.EOF);
    }

    [Fact]
    public void LexCharacter_ShouldReturnInvalid()
    {
        var lexer = new Lexer("'a");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "'a");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Invalid, TokenType.EOF);
    }
}