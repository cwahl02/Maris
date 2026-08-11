using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void LexProgram_HelloWorld()
    {
        Equal(
            """
            import std.io as io;

            main :: () -> i32 {
                io.print("Hello, World!");
                return 0;
            }
            """,
                (TokenType.Import, "import"),
                (TokenType.Identifier, "std"),
                (TokenType.Dot, "."),
                (TokenType.Identifier, "io"),
                (TokenType.As, "as"),
                (TokenType.Identifier, "io"),
                (TokenType.Semicolon, ";"),

                (TokenType.Identifier, "main"),
                (TokenType.ColonColon, "::"),
                (TokenType.LeftParen, "("),
                (TokenType.RightParen, ")"),
                (TokenType.Arrow, "->"),
                (TokenType.I32, "i32"),
                (TokenType.LeftBrace, "{"),

                (TokenType.Identifier, "io"),
                (TokenType.Dot, "."),
                (TokenType.Identifier, "print"),
                (TokenType.LeftParen, "("),
                (TokenType.StringLiteral, "\"Hello, World!\""),
                (TokenType.RightParen, ")"),
                (TokenType.Semicolon, ";"),

                (TokenType.Return, "return"),
                (TokenType.IntegerLiteral, "0"),
                (TokenType.Semicolon, ";"),

                (TokenType.RightBrace, "}"),

                (TokenType.EOF, "")
        );
    }
}