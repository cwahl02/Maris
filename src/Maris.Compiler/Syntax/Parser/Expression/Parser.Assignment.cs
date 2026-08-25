using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record AssignmentExpressionSyntax(
    ExpressionSyntax Left,
    SyntaxToken OperatorToken,
    ExpressionSyntax Right
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParseAssignmentExpression()
    {
        ExpressionSyntax left = ParseLogicalOrExpression();

        SyntaxTokenKind kind = _iterator.Current.Kind;
        if (kind is SyntaxTokenKind.Equal or
            SyntaxTokenKind.PlusEqual or
            SyntaxTokenKind.MinusEqual or
            SyntaxTokenKind.StarEqual or
            SyntaxTokenKind.SlashEqual or
            SyntaxTokenKind.PercentEqual or
            SyntaxTokenKind.CaretEqual or
            SyntaxTokenKind.AmpersandEqual or
            SyntaxTokenKind.PipeEqual or
            SyntaxTokenKind.LeftShiftEqual or
            SyntaxTokenKind.RightShiftEqual or
            SyntaxTokenKind.ColonEqual or
            SyntaxTokenKind.ColonColonEqual)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();

            ExpressionSyntax right = ParseAssignmentExpression();
            return new AssignmentExpressionSyntax(left, operatorToken, right);
        }

        return left;
    }
}