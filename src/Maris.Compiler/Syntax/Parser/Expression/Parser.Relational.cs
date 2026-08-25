using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseRelationalExpression()
    {
        ExpressionSyntax expr = ParseAdditiveExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.LessThan ||
               _iterator.Current.Kind == SyntaxTokenKind.LessThanEqual ||
               _iterator.Current.Kind == SyntaxTokenKind.GreaterThan ||
               _iterator.Current.Kind == SyntaxTokenKind.GreaterThanEqual)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();
            
            ExpressionSyntax right = ParseAdditiveExpression();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}