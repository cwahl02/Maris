// using Maris.Compiler.Lexing;

// namespace Maris.Tests.Compiler.Lexing;

// public class Ampersand
// {
//     [Fact]
//     public void Lex_Ampersand()
//     {
//         var lexer = new Lexer("& && &=");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "&", "&&", "&=");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.Ampersand, TokenType.AmpersandAmpersand, TokenType.AmpersandEqual, TokenType.EOF);
//     }

//     [Fact]
//     public void Lex_Ampersand_NoWhitespace()
//     {
//         var lexer = new Lexer("&&&=&");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "&&", "&=", "&");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.AmpersandAmpersand, TokenType.AmpersandEqual, TokenType.Ampersand, TokenType.EOF);
//     }
// }