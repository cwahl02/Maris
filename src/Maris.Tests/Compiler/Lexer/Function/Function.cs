using Maris.Compiler.Lexer;

public partial class LexerTests
{
    [Fact]
    public void LexFunction_FunctionDeclaration_ShouldReturnTokens()
    {
        var lexer = new Lexer(
            """
            add :: (x, y: i32) -> i32 {
                return x + y;
            }
            """);
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "add", "::", "(", "x", ",", "y", ":", "i32", ")", "->", "i32", "{", "return", "x", "+", "y", ";", "}", "");
        LexerAssert.ContainsTokenTypes(tokens,
            TokenType.Identifier,
            TokenType.ColonColon,
            TokenType.LeftParen,
            TokenType.Identifier,
            TokenType.Comma,
            TokenType.Identifier,
            TokenType.Colon,
            TokenType.I32,
            TokenType.RightParen,
            TokenType.Arrow,
            TokenType.I32,
            TokenType.LeftBrace,

            TokenType.Return,
            TokenType.Identifier,
            TokenType.Plus,
            TokenType.Identifier,
            TokenType.Semicolon,

            TokenType.RightBrace,
            TokenType.EOF
        );
    }

    [Fact]
    public void LexFunction_FunctionDeclarationWithDefaultParams_ShouldReturnTokens()
    {
        var lexer = new Lexer(
            """
            myfunc :: (x: i32 = 27) -> i32 {
                return x * x;
            }
            """);
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "myfunc", "::", "(", "x", ":", "i32", "=", "27", ")", "->", "i32", "{", "return", "x", "*", "x", ";", "}", "");
        LexerAssert.ContainsTokenTypes(tokens,
            TokenType.Identifier,
            TokenType.ColonColon,
            TokenType.LeftParen,
            TokenType.Identifier,
            TokenType.Colon,
            TokenType.I32,
            TokenType.Equal,
            TokenType.IntegerLiteral,
            TokenType.RightParen,
            TokenType.Arrow,
            TokenType.I32,
            TokenType.LeftBrace,

            TokenType.Return,
            TokenType.Identifier,
            TokenType.Star,
            TokenType.Identifier,
            TokenType.Semicolon,

            TokenType.RightBrace,
            TokenType.EOF
        );
    }
}