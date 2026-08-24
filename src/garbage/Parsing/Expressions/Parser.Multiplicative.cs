namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseMultiplicativeExpression()
    {
        var left = ParseUnaryExpression();

        while (_iterator.Current.Kind is Lexing.TokenKind.Star or Lexing.TokenKind.Slash or Lexing.TokenKind.Percent)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var right = ParseUnaryExpression();

            left = new BinaryExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }
}