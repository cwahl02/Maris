using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseMultiplicative()
    {
        var expr = ParseUnary();

        while (_iterator.Current.Kind == SyntaxTokenKind.Star ||
               _iterator.Current.Kind == SyntaxTokenKind.Slash ||
               _iterator.Current.Kind == SyntaxTokenKind.Percent)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseUnary();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}