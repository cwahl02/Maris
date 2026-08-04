using Maris.Compiler.Lexing;
using Maris.Compiler.Text;
using Xunit;

namespace Maris.Tests;

public class LexerTests_Operators
{
    [Fact]
    public void LexerTests_LexSingleCharacterOperators()
    {
        // Arrange
        var source = "+ - * / % = < > ! & | ^ ~ @ #";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);
        var expectedTokens = new List<TokenType>
        {
            TokenType.Plus,
            TokenType.Minus,
            TokenType.Star,
            TokenType.Slash,
            TokenType.Percent,
            TokenType.Equal,
            TokenType.Less,
            TokenType.Greater,
            TokenType.Bang,
            TokenType.Ampersand,
            TokenType.Pipe,
            TokenType.Caret,
            TokenType.Tilde,
            TokenType.At,
            TokenType.Hash,
            TokenType.EndOfFile
        };

        // Act
        var tokens = lexer.Tokenize();     

        // Assert
        Assert.Equal(expectedTokens.Count, tokens.Count);
        Assert.Equal(expectedTokens, tokens.Select(t => t.Type).ToList());
    }

    [Fact]
    public void LexerTests_LexMultiCharacterOperators()
    {
        // Arrange
        var source = "== += -= *= /= %= ^= &= |= != << <= <<= >> >= >>= :: && || ..";
        var sourceFile = new SourceFile("test", source);
        var lexer = new Lexer(sourceFile);
        var expectedTokens = new List<TokenType>
        {
            TokenType.EqualEqual,
            TokenType.PlusEqual,
            TokenType.MinusEqual,
            TokenType.StarEqual,
            TokenType.SlashEqual,
            TokenType.PercentEqual,
            TokenType.CaretEqual,
            TokenType.AmpersandEqual,
            TokenType.PipeEqual,
            TokenType.BangEqual,
            TokenType.LeftShift,
            TokenType.LessEqual,
            TokenType.LeftShiftEqual,
            TokenType.RightShift,
            TokenType.GreaterEqual,
            TokenType.RightShiftEqual,
            TokenType.ColonColon,
            TokenType.AmpersandAmpersand,
            TokenType.PipePipe,
            TokenType.Range,
            TokenType.EndOfFile
        };

        // Act
        var tokens = lexer.Tokenize();

        // Assert
        Assert.Equal(expectedTokens.Count, tokens.Count);
        Assert.Equal(expectedTokens, tokens.Select(t => t.Type).ToList());
    }
}