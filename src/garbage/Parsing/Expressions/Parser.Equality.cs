namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseEqualityExpression()
    {
        var left = ParseComparisonExpression();

        while (_iterator.Current.Kind == Lexing.TokenKind.EqualEqual || _iterator.Current.Kind == Lexing.TokenKind.BangEqual)
        {
            var operatorToken = _iterator.Current.Kind == Lexing.TokenKind.EqualEqual
                ? Match(Lexing.TokenKind.EqualEqual)
                : Match(Lexing.TokenKind.BangEqual);
            var right = ParseComparisonExpression();

            left = new BinaryExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }
}