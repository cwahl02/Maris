// using Maris.Compiler.Lexing;

// namespace Maris.Tests.Compiler.Lexing;

// public class Enum
// {
//     [Fact]
//     public void LexProgram_Enum_ShouldReturnTokens()
//     {

//         var lexer = new Lexer(
//             """
//             Color :: enum {
//                 Red,
//                 Green,
//                 Blue
//             }
//             """);
//         var tokens = lexer.Lex();

//         LexerAssert.DoesNotContainTokenTypeInvalid(tokens);
//         LexerAssert.ContainsTokenTypes(tokens,
//             TokenType.Identifier,
//             TokenType.ColonColon,
//             TokenType.Enum,
//             TokenType.LeftBrace,
//             TokenType.Identifier,
//             TokenType.Comma,
//             TokenType.Identifier,
//             TokenType.Comma,
//             TokenType.Identifier,
//             TokenType.RightBrace,
//             TokenType.EOF
//         );
//     }

//     [Fact]
//     public void LexProgram_EnumWithValues_ShouldReturnTokens()
//     {
//         var lexer = new Lexer(
//             """
//             Color :: enum {
//                 Red = 1,
//                 Green = 2,
//                 Blue = 3
//             }
//             """);
//         var tokens = lexer.Lex();

//         LexerAssert.DoesNotContainTokenTypeInvalid(tokens);
//         LexerAssert.ContainsTokenTypes(tokens,
//             TokenType.Identifier,
//             TokenType.ColonColon,
//             TokenType.Enum,
//             TokenType.LeftBrace,
//             TokenType.Identifier,
//             TokenType.Equal,
//             TokenType.IntegerLiteral,
//             TokenType.Comma,
//             TokenType.Identifier,
//             TokenType.Equal,
//             TokenType.IntegerLiteral,
//             TokenType.Comma,
//             TokenType.Identifier,
//             TokenType.Equal,
//             TokenType.IntegerLiteral,
//             TokenType.RightBrace,
//             TokenType.EOF
//         );
//     }
// }