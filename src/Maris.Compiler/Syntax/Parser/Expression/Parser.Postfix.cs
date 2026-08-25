using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record CallExpressionSyntax(
    ExpressionSyntax Callee,
    SyntaxToken LeftParen,
    ExpressionListSyntax Arguments,
    SyntaxToken RightParen
) : ExpressionSyntax;

public sealed record MemberAccessExpressionSyntax(
    ExpressionSyntax Expression,
    SyntaxToken DotToken,
    SyntaxToken IdentifierToken
) : ExpressionSyntax;

public sealed record IndexExpressionSyntax(
    SyntaxToken LeftBracket,
    ExpressionSyntax Expression,
    ExpressionSyntax Index,
    SyntaxToken RightBracket
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParsePostfixExpression()
    {
        ExpressionSyntax expr = ParsePrimaryExpression();

        while (true)
        {
            switch (_iterator.Current.Kind)
            {
                case SyntaxTokenKind.LeftParen:
                    expr = ParseCallExpression(expr);
                    break;

                case SyntaxTokenKind.Dot:
                    expr = ParseMemberAccessExpression(expr);
                    break;

                case SyntaxTokenKind.LeftBracket:
                    expr = ParseIndexExpression(expr);
                    break;

                default:
                    return expr;
            }
        }
    }
}