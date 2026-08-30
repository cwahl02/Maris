using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record WhileStatement(
    SyntaxToken WhileKeyword,
    ExpressionSyntax Condition,
    BlockSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    private WhileStatement ParseWhileStatement()
    {
        SyntaxToken whileKeyword = Expect(SyntaxTokenKind.While);
        ExpressionSyntax condition = ParseExpression();
        BlockSyntax body = ParseBlock();

        return new WhileStatement(
            whileKeyword,
            condition,
            body
        );
    }
}