namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParsePostfixExpression()
    {
        var expression = ParsePrimaryExpression();

        while(true)
        {
            if (Match(Lexing.TokenKind.LeftParen))
            {
                var arguments = ParseArgumentList();
                var rightParen = Match(Lexing.TokenKind.RightParen);

                expression = new CallExpressionSyntax(
                    expression,
                    arguments,
                    rightParen
                );
            }
            else if (Match(Lexing.TokenKind.LeftBracket))
            {
                var indexExpression = ParseExpression();
                var rightBracket = Match(Lexing.TokenKind.RightBracket);

                expression = new IndexExpressionSyntax(
                    expression,
                    indexExpression,
                    rightBracket
                );
            }
            else
            {
                break;
            }
        }
    }
}