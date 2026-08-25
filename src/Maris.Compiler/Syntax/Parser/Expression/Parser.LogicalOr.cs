using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseLogicalOrExpression()
    {
        ExpressionSyntax expr = ParseLogicalAndExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.PipePipe)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();
            
            ExpressionSyntax right = ParseLogicalAndExpression();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }

        return expr;
    }
}