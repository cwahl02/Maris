using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;


namespace Maris.Tests.Compiler.Syntax.Lexing;

public class HelloWorld1
{
    private readonly ITestOutputHelper _output;
    public HelloWorld1(ITestOutputHelper output)
    {
        _output = output;
    }
//     [Fact]
//     public void LexProgram_HelloWorld_ShouldReturnTokens()
//     {
//         var lexer = new Lexer(
//             """
//             import std.io;

//             main :: () -> i32 {
//                 print("Hello, World!");
//                 return 0;
//             }
//             """);
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "import", "std", "io", "main", "print", "return");
//         LexerAssert.ContainsTokenTypes(tokens,
//             TokenType.Import,
//             TokenType.Identifier,
//             TokenType.Dot,
//             TokenType.Identifier,
//             TokenType.Semicolon,

//             TokenType.Identifier,
//             TokenType.ColonColon,
//             TokenType.LeftParen,
//             TokenType.RightParen,
//             TokenType.Arrow,
//             TokenType.I32,
//             TokenType.LeftBrace,

//             TokenType.Identifier,
//             TokenType.LeftParen,
//             TokenType.StringLiteral,
//             TokenType.RightParen,
//             TokenType.Semicolon,

//             TokenType.Return,
//             TokenType.IntegerLiteral,
//             TokenType.Semicolon,
//             TokenType.RightBrace,
//             TokenType.EOF
//         );
//     }

    [Fact]
    public void LexProgram_HelloWorld_ShouldNotContainInvalidTokens()
    {
        var text =
            """
            import std.io;

            main :: () -> i32 {
                print("Hello, World!");
                return 0;
            }
            """
        ;
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        IReadOnlyList<Token> tokens = lexer.Lex();

        foreach (var token in tokens)
        {
            _output.WriteLine($"Token: {token.Kind}, Text: '{text.Substring(token.Span.Start, token.Span.Length)}'");
        }

        Assert.True(tokens.Contains(TokenKind.Invalid) == false, "Tokens should not contain any Invalid tokens.");
        Assert.True(tokens.Contains(text, "import", "std", "io", "main", "print", "return"));
    }
}