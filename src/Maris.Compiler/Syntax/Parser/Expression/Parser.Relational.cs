using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseRelational()
    {
        var expr = ParseAdditive();

        while (_iterator.Current.Kind == SyntaxTokenKind.LessThan ||
               _iterator.Current.Kind == SyntaxTokenKind.LessThanEqual ||
               _iterator.Current.Kind == SyntaxTokenKind.GreaterThan ||
               _iterator.Current.Kind == SyntaxTokenKind.GreaterThanEqual)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseAdditive();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}