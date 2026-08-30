using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record WhileStatement(
    SyntaxToken WhileKeyword,
    ExpressionSyntax Condition,
    StatementSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    private WhileStatement ParseWhileStatement()
    {
        SyntaxToken whileKeyword = Expect(SyntaxTokenKind.While);
        ExpressionSyntax condition = ParseExpression();
        StatementSyntax body = ParseControlBody();

        return new WhileStatement(
            whileKeyword,
            condition,
            body
        );
    }
}
