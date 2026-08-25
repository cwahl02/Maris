using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record UnaryExpressionSyntax(
    SyntaxToken OperatorToken,
    ExpressionSyntax Operand
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParseUnaryExpression()
    {
        SyntaxTokenKind kind = _iterator.Current.Kind;
        if (kind == SyntaxTokenKind.Plus ||
            kind == SyntaxTokenKind.Minus ||
            kind == SyntaxTokenKind.Star ||
            kind == SyntaxTokenKind.Ampersand ||
            kind == SyntaxTokenKind.Bang)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();

            ExpressionSyntax operand = ParseUnaryExpression();
            return new UnaryExpressionSyntax(operatorToken, operand);
        }

        return ParsePostfixExpression();
    }
}