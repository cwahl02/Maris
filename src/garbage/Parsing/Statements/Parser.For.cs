namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseForStatement()
    {
        var forKeyword = Match(Lexing.TokenKind.For);
        //var expression = ParseExpression();
        var controlBody = ParseControlBody();

        return new ForStatementSyntax(
            forKeyword,
            //expression,
            controlBody
        );
    }

    public sealed record ForStatementSyntax(
        Lexing.Token ForKeyword,
        //SyntaxNode Expression,
        SyntaxNode? ControlBody
    ) : SyntaxNode;
}