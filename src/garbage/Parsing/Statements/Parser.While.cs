namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseWhileStatement()
    {
        var whileKeyword = Match(Lexing.TokenKind.While);
        //var expression = ParseExpression();
        var controlBody = ParseControlBody();

        return new WhileStatementSyntax(
            whileKeyword,
            //expression,
            controlBody
        );
    }
}