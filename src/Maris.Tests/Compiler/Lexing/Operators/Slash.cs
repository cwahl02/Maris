// using Maris.Compiler.Lexing;

// namespace Maris.Tests.Compiler.Lexing;

// public class Slash
// {
//     [Fact]
//     public void Lex_Slash()
//     {
//         var lexer = new Lexer("/ /=");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "/", "/=");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.Slash, TokenType.SlashEqual, TokenType.EOF);
//     }

//     [Fact]
//     public void Lex_Slash_NoWhitespace()
//     {
//         var lexer = new Lexer("/=/");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "/=", "/");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.SlashEqual, TokenType.Slash, TokenType.EOF);
//     }
// }