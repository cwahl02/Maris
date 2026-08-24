using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record UnaryExpressionSyntax(
    SyntaxToken OperatorToken,
    ExpressionSyntax Operand
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParseUnary()
    {
        if (_iterator.Current.Kind == SyntaxTokenKind.Plus ||
            _iterator.Current.Kind == SyntaxTokenKind.Minus ||
            _iterator.Current.Kind == SyntaxTokenKind.Bang)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var operand = ParseUnary();
            return new UnaryExpressionSyntax(operatorToken, operand);
        }

        return ParsePostfix();
    }
}