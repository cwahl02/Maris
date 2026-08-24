using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseLogicalOr()
    {
        var expr = ParseLogicalAnd();

        while (_iterator.Current.Kind == SyntaxTokenKind.PipePipe)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseLogicalAnd();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }

        return expr;
    }
}