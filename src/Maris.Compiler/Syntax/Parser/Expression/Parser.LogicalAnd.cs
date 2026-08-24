using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseLogicalAnd()
    {
        var expr = ParseEquality();

        while (_iterator.Current.Kind == SyntaxTokenKind.AmpersandAmpersand)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseEquality();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}