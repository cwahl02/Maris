using Maris.Compiler.Lexer;

public partial class LexerTests
{
    [Fact]
    public void LexProgram_Enum_ShouldReturnTokens()
    {
        Equal(
            """
            Color :: enum {
                Red,
                Green,
                Blue
            }
            """,
            (TokenType.Identifier, "Color"),
            (TokenType.ColonColon, "::"),
            (TokenType.Enum, "enum"),
            (TokenType.LeftBrace, "{"),

            (TokenType.Identifier, "Red"),
            (TokenType.Comma, ","),

            (TokenType.Identifier, "Green"),
            (TokenType.Comma, ","),

            (TokenType.Identifier, "Blue"),

            (TokenType.RightBrace, "}"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexProgram_EnumWithValues_ShouldReturnTokens()
    {
        Equal(
            """
            Color :: enum {
                Red = 1,
                Green = 2,
                Blue = 3
            }
            """,
            (TokenType.Identifier, "Color"),
            (TokenType.ColonColon, "::"),
            (TokenType.Enum, "enum"),
            (TokenType.LeftBrace, "{"),

            // Red = 1,
            (TokenType.Identifier, "Red"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "1"),
            (TokenType.Comma, ","),

            // Green = 2,
            (TokenType.Identifier, "Green"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "2"),
            (TokenType.Comma, ","),

            // Blue = 3
            (TokenType.Identifier, "Blue"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "3"),

            (TokenType.RightBrace, "}"),

            (TokenType.EOF, "")
        );
    }
}