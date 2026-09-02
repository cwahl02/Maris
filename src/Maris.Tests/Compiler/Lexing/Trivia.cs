using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public class Trivia
{
    [Fact]
    public void Lex_BlockComment_IsSkippedAndFollowingTokenIsLexed()
    {
        var lexer = new Lexer("/* comment */ main");
        var tokens = lexer.Lex();

        LexerAssert.DoesNotContainTokenTypeInvalid(tokens);
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Identifier, TokenType.EOF);
        Assert.Equal(2, tokens.Count);
        Assert.Equal("main", tokens[0].Value.ToString());
    }

    [Fact]
    public void Lex_NestedBlockComments_AreFullySkipped()
    {
        var lexer = new Lexer("/* outer /* inner */ still comment */ main");
        var tokens = lexer.Lex();

        LexerAssert.DoesNotContainTokenTypeInvalid(tokens);
        Assert.Equal(2, tokens.Count);
        Assert.Equal("main", tokens[0].Value.ToString());
        Assert.Equal(TokenType.EOF, tokens[1].Type);
    }

    [Fact]
    public void Lex_BlockCommentFollowedByWhitespaceAndMoreCode_LexesAllTokens()
    {
        var lexer = new Lexer("/* comment */\n\nmain :: ();");
        var tokens = lexer.Lex();

        LexerAssert.DoesNotContainTokenTypeInvalid(tokens);
        LexerAssert.ContainsTokenTypes(tokens,
            TokenType.Identifier,
            TokenType.ColonColon,
            TokenType.LeftParen,
            TokenType.RightParen,
            TokenType.Semicolon,
            TokenType.EOF);
    }
}
