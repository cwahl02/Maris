using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseLogicalAndExpression()
    {
        ExpressionSyntax expr = ParseEqualityExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.AmpersandAmpersand)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();

            ExpressionSyntax right = ParseEqualityExpression();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}