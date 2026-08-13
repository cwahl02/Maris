using Maris.Compiler.Lexer;

public partial class LexerTests
{
    [Fact]
    public void LexFunction_FunctionDeclaration_ShouldReturnTokens()
    {
        Equal(
            """
            add :: (x, y: i32) -> i32 {
                return x + y;
            }
            """,
            (TokenType.Identifier, "add"),
            (TokenType.ColonColon, "::"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "x"),
            (TokenType.Comma, ","),
            (TokenType.Identifier, "y"),
            (TokenType.Colon, ":"),
            (TokenType.I32, "i32"),
            (TokenType.RightParen, ")"),
            (TokenType.Arrow, "->"),
            (TokenType.I32, "i32"),
            (TokenType.LeftBrace, "{"),

            (TokenType.Return, "return"),
            (TokenType.Identifier, "x"),
            (TokenType.Plus, "+"),
            (TokenType.Identifier, "y"),
            (TokenType.Semicolon, ";"),

            (TokenType.RightBrace, "}"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void LexFunction_FunctionDeclarationWithDefaultParams_ShouldReturnTokens()
    {
        Equal(
            """
            myfunc :: (x: i32 = 27) -> i32 {
                return x * x;
            }
            """,
            (TokenType.Identifier, "myfunc"),
            (TokenType.ColonColon, "::"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "x"),
            (TokenType.Colon, ":"),
            (TokenType.I32, "i32"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "27"),
            (TokenType.RightParen, ")"),
            (TokenType.Arrow, "->"),
            (TokenType.I32, "i32"),
            (TokenType.LeftBrace, "{"),

            (TokenType.Return, "return"),
            (TokenType.Identifier, "x"),
            (TokenType.Star, "*"),
            (TokenType.Identifier, "x"),
            (TokenType.Semicolon, ";"),

            (TokenType.RightBrace, "}"),
            (TokenType.EOF, "")
        );
    }
}