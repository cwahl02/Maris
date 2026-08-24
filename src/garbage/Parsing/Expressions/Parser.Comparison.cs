namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseComparisonExpression()
    {
        var left = ParseAdditiveExpression();

        while (_iterator.Current.Kind == Lexing.TokenKind.LessThan || _iterator.Current.Kind == Lexing.TokenKind.LessThanEqual || _iterator.Current.Kind == Lexing.TokenKind.GreaterThan || _iterator.Current.Kind == Lexing.TokenKind.GreaterThanEqual)
        {
            var operatorToken = _iterator.Current.Kind switch
            {
                Lexing.TokenKind.LessThan => Match(Lexing.TokenKind.LessThan),
                Lexing.TokenKind.LessThanEqual => Match(Lexing.TokenKind.LessThanEqual),
                Lexing.TokenKind.GreaterThan => Match(Lexing.TokenKind.GreaterThan),
                Lexing.TokenKind.GreaterThanEqual => Match(Lexing.TokenKind.GreaterThanEqual),
                _ => throw new InvalidOperationException("Unexpected token kind.")
            };

            var right = ParseAdditiveExpression();

            left = new BinaryExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }
}