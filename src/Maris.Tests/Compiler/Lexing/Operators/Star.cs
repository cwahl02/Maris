// using Maris.Compiler.Lexing;

// namespace Maris.Tests.Compiler.Lexing;

// public class Star
// {
//     [Fact]
//     public void Lex_Star()
//     {
//         var lexer = new Lexer("* *=");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "*", "*=");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.Star, TokenType.StarEqual, TokenType.EOF);
//     }

//     [Fact]
//     public void Lex_Star_NoWhitespace()
//     {
//         var lexer = new Lexer("**=*");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "*", "*=", "*");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.Star, TokenType.StarEqual, TokenType.Star, TokenType.EOF);
//     }
// }