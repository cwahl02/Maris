namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseAssignmentExpression()
    {
        var left = ParseLogicalOrExpression();

        if (IsAssignmentOperator(_iterator.Current.Kind))
        {
            var operatorToken = Match(_iterator.Current.Kind);
            var right = ParseAssignmentExpression();

            return new AssignmentExpressionSyntax(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }

    private bool IsAssignmentOperator(Lexing.TokenKind kind)
    {
        return kind switch 
        {
            Lexing.TokenKind.Equal => true,
            Lexing.TokenKind.PlusEqual => true,
            Lexing.TokenKind.MinusEqual => true,
            Lexing.TokenKind.StarEqual => true,
            Lexing.TokenKind.SlashEqual => true,
            _ => false
        };
    }
}