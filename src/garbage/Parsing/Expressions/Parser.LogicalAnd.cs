namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseLogicalAndExpression()
    {
        var left = ParseEqualityExpression();

        while (_iterator.Current.Kind == Lexing.TokenKind.AmpersandAmpersand)
        {
            var operatorToken = Match(Lexing.TokenKind.AmpersandAmpersand);
            var right = ParseEqualityExpression();

            left = new BinaryExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }
}