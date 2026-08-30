namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseBreakStatement()
    {
        var breakKeyword = Match(Lexing.TokenKind.Break);
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new BreakStatementSyntax(
            breakKeyword,
            semicolon
        );
    }

    public sealed record BreakStatementSyntax(
        Lexing.Token BreakKeyword,
        Lexing.Token Semicolon
    ) : SyntaxNode;
}