namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseEmptyStatement()
    {
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new EmptyStatementSyntax(
            semicolon
        );
    }

    public sealed record EmptyStatementSyntax(
        Lexing.Token Semicolon
    ) : SyntaxNode;
}