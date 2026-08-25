using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseAdditiveExpression()
    {
        ExpressionSyntax expr = ParseMultiplicativeExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.Plus ||
               _iterator.Current.Kind == SyntaxTokenKind.Minus)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();

            ExpressionSyntax right = ParseMultiplicativeExpression();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}