// using Maris.Compiler.Lexing;

// namespace Maris.Tests.Compiler.Lexing;

// public partial class LexerTests
// {
//     [Fact]
//     public void LexNumber_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer("123.0");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "123.0");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatStartingWithDot_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer(".123");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, ".123");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithExponent_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer("1.23e4");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "1.23e4");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithExponentUppercase_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer("1.23E4");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "1.23E4");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithExponentAndSign_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer("1.23e-4");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "1.23e-4");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithExponentAndPositiveSign_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer("1.23e+4");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "1.23e+4");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithLeadingAndTrailingDot_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer("123.");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, "123.");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithLeadingDot_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer(".123");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, ".123");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithLeadingDotAndExponent_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer(".123e4");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, ".123e4");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }

//     [Fact]
//     public void LexNumber_FloatWithLeadingDotAndExponentAndSign_ShouldReturnFloatLiteral()
//     {
//         var lexer = new Lexer(".123e-4");
//         var tokens = lexer.Lex();

//         LexerAssert.ContainsText(tokens, ".123e-4");
//         LexerAssert.ContainsTokenTypes(tokens, TokenType.FloatLiteral, TokenType.EOF);
//     }
// }