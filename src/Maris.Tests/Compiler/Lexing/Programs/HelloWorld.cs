// using Maris.Compiler.Lexing;

// namespace Maris.Tests.Compiler.Lexing;

// public class HelloWorld1
// {
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

//     [Fact]
//     public void LexProgram_HelloWorld_ShouldNotContainInvalidTokens()
//     {
//         var lexer = new Lexer(
//             """
//             import std.io;

//             main :: () -> i32 {
//                 print("Hello, World!");
//                 return 0;
//             }
//             """
//         );
//         var tokens = lexer.Lex();

//         LexerAssert.DoesNotContainTokenTypeInvalid(tokens);
//         LexerAssert.ContainsText(tokens, "import", "std", "io", "main", "print", "return");
//     }
// }