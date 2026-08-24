using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private ExpressionSyntax ParseEquality()
    {
        var expr = ParseRelational();

        while (_iterator.Current.Kind == SyntaxTokenKind.EqualEqual ||
               _iterator.Current.Kind == SyntaxTokenKind.BangEqual)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseRelational();
            expr = new BinaryExpressionSyntax(expr, operatorToken, right);
        }
        
        return expr;
    }
}