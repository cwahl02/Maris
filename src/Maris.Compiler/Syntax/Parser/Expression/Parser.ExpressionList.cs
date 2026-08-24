using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ExpressionListSyntax(
    IReadOnlyList<ExpressionSyntax> Expressions
) : StatementSyntax;

public sealed partial class Parser
{
    private ExpressionListSyntax ParseExpressionList()
    {
        List<ExpressionSyntax> expressions = new();

        do
        {
            expressions.Add(ParseExpression());
        } while (_iterator.Current.Kind == SyntaxTokenKind.Comma);

        return new ExpressionListSyntax(expressions);
    }
}