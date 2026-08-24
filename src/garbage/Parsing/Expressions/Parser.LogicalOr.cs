namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseLogicalOrExpression()
    {
        var left = ParseLogicalAndExpression();

        while (_iterator.Current.Kind == Lexing.TokenKind.PipePipe)
        {
            var operatorToken = Match(Lexing.TokenKind.PipePipe);
            var right = ParseLogicalAndExpression();

            left = new BinaryExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }
}