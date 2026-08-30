namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseExpressionList()
    {
        var expressions = new List<SyntaxNode>();

        while (true)
        {
            var expression = ParseExpression();
            expressions.Add(expression);

            if (_iterator.Current.Kind == Lexing.TokenKind.Comma)
            {
                Match(Lexing.TokenKind.Comma);
            }
            else
            {
                break;
            }
        }

        return new ExpressionListSyntax(
            expressions
        );
    }
}