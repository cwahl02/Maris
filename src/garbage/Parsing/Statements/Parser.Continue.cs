namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseContinueStatement()
    {
        var continueKeyword = Match(Lexing.TokenKind.Continue);
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new ContinueStatementSyntax(
            continueKeyword,
            semicolon
        );
    }

    public sealed record ContinueStatementSyntax(
        Lexing.Token ContinueKeyword,
        Lexing.Token Semicolon
    ) : SyntaxNode;
}