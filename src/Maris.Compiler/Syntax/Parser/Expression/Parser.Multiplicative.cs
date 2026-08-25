using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseMultiplicativeExpression()
    {
        ExpressionSyntax expr = ParseUnaryExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.Star ||
               _iterator.Current.Kind == SyntaxTokenKind.Slash ||
               _iterator.Current.Kind == SyntaxTokenKind.Percent)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();

            ExpressionSyntax right = ParseUnaryExpression();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}