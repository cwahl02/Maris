using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record BinaryExpressionSyntax(
    ExpressionSyntax Left,
    SyntaxToken OperatorToken,
    ExpressionSyntax Right
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParseExpression()
    {
        return ParseAssignmentExpression();
    }
}