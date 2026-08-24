using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record WhileSyntax(
    SyntaxToken WhileKeyword,
    ExpressionSyntax Condition,
    StatementSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    private WhileSyntax ParseWhile()
    {
        var whileKeyword = Expect(SyntaxTokenKind.While);
        var condition = ParseExpression();
        var body = ParseControlFlowBody();

        return new WhileSyntax(whileKeyword, condition, body);
    }
}