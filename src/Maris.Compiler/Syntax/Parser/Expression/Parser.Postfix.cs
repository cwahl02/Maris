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
    private ExpressionSyntax ParsePostfix()
    {
        var expr = ParsePrimary();

        while (true)
        {
            if (_iterator.Current.Kind == SyntaxTokenKind.LeftParen)
            {
                var openParenToken = _iterator.Current;
                var arguments = ParseExpressionList();
                var closeParenToken = Expect(SyntaxTokenKind.RightParen);
                expr = new CallExpressionSyntax(expr, openParenToken, arguments, closeParenToken);
                continue;
            }
            else if (_iterator.Current.Kind == SyntaxTokenKind.Dot)
            {
                var dotToken = _iterator.Current;
                _iterator.Forward();
                var identifierToken = Expect(SyntaxTokenKind.Identifier);
                expr = new MemberAccessExpressionSyntax(expr, dotToken, identifierToken);
                continue;
            }
            
            if (_iterator.Current.Kind == SyntaxTokenKind.LeftBracket)
            {
                var leftBracket = _iterator.Current;
                _iterator.Forward();
                var index = ParseExpression();
                var rightBracket = Expect(SyntaxTokenKind.RightBracket);
                expr = new IndexExpressionSyntax(leftBracket, expr, index, rightBracket);
                continue;
            }

            break;
        }
        
        return expr;
    }
}