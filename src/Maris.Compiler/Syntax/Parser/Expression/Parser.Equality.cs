using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseEqualityExpression()
    {
        ExpressionSyntax expr = ParseRelationalExpression();

        while (_iterator.Current.Kind == SyntaxTokenKind.EqualEqual ||
               _iterator.Current.Kind == SyntaxTokenKind.BangEqual)
        {
            SyntaxToken operatorToken = _iterator.Current;
            _iterator.Forward();
            
            ExpressionSyntax right = ParseRelationalExpression();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}