using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ForStatement(
    SyntaxToken ForKeyword,
    StatementSyntax Initializer,
    ExpressionSyntax Condition,
    StatementSyntax Iterator,
    BlockSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    private ForStatement ParseForStatement()
    {
        SyntaxToken forKeyword = Expect(SyntaxTokenKind.For);
        StatementSyntax initializer = ParseStatement();
        ExpressionSyntax condition = ParseExpression();
        StatementSyntax iterator = ParseStatement();
        BlockSyntax body = ParseBlock();

        return new ForStatement(
            forKeyword,
            initializer,
            condition,
            iterator,
            body
        );
    }
}