namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseReturnStatement()
    {
        var returnKeyword = Match(Lexing.TokenKind.Return);
        var expressionList = ParseExpressionList();
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new ReturnStatementSyntax(
            returnKeyword,
            expressionList,
            semicolon
        );
    }

    public sealed record ReturnStatementSyntax(
        Lexing.Token ReturnKeyword,
        ExpressionListSyntax ExpressionList,
        Lexing.Token Semicolon
    ) : SyntaxNode;
}