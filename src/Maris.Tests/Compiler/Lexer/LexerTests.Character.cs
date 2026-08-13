using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexCharacter_ShouldReturnCharacterLiteralToken()
    {
        Equal(
            "'a'",
            (TokenType.CharacterLiteral, "'a'"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexCharacter_ShouldReturnInvalid()
    {
        Equal(
            "'a",
            (TokenType.Invalid, "'a"),
            (TokenType.EOF, "")
        );
    }
}