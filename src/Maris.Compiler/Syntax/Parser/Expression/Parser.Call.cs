using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record CallSyntax(
    ExpressionSyntax Callee,
    ExpressionListSyntax Arguments
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParseCall()
    {
        var expr = ParsePostfixExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.LeftParen)
        {
            var openParenToken = _iterator.Current;
            _iterator.Forward();
            var arguments = ParseExpressionList();
            var closeParenToken = _iterator.Current;
            _iterator.Forward();
            expr = new CallSyntax(expr, arguments);
        }

        return expr;

    }
}