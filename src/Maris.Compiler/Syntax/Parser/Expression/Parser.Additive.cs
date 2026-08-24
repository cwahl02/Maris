using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseAdditive()
    {
        var expr = ParseMultiplicative();

        while (_iterator.Current.Kind == SyntaxTokenKind.Plus ||
               _iterator.Current.Kind == SyntaxTokenKind.Minus)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseMultiplicative();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}