namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseAdditiveExpression()
    {
        var left = ParseMultiplicativeExpression();

        while (_iterator.Current.Kind is Lexing.TokenKind.Plus or Lexing.TokenKind.Minus)
        {
            var operatorToken = _iterator.Current;
            _iterator.Advance();
            var right = ParseMultiplicativeExpression();

            left = new BinaryExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }
}