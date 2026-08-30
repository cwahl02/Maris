using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ExpressionStatement(
    SeparatedSyntax<ExpressionSyntax> Expressions,
    SyntaxToken Semicolon
) : StatementSyntax;

public sealed record AssignmentExpression(
    ExpressionSyntax Left,
    SyntaxToken EqualToken,
    ExpressionSyntax Right
) : ExpressionSyntax;

public sealed record BinaryExpression(
    ExpressionSyntax Left,
    SyntaxToken OperatorToken,
    ExpressionSyntax Right
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionStatement ParseExpressionStatement()
    {
        SeparatedSyntax<ExpressionSyntax> expressions = ParseSeparated(ParseExpression, SyntaxTokenKind.Comma);
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new ExpressionStatement(
            expressions,
            semicolon
        );
    }

    // ==================== Precedence Climbing ====================

    private ExpressionSyntax ParseExpression() => ParseAssignmentExpression();

    private ExpressionSyntax ParseAssignmentExpression()
    {
        ExpressionSyntax left = ParseLogicalOrExpression();

        if (!Match(SyntaxTokenKind.Equal))
        {
            return left;
        }

        SyntaxToken equalToken = Previous;
        ExpressionSyntax right = ParseAssignmentExpression();

        return new AssignmentExpression(
            left,
            equalToken,
            right
        );
    }

    private ExpressionSyntax ParseLogicalOrExpression()
    {
        ExpressionSyntax left = ParseLogicalAndExpression();

        while (Match(SyntaxTokenKind.Or))
        {
            SyntaxToken orToken = Previous;
            ExpressionSyntax right = ParseLogicalAndExpression();

            left = new BinaryExpression(
                left,
                orToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParseLogicalAndExpression()
    {
        ExpressionSyntax left = ParseEqualityExpression();

        while (Match(SyntaxTokenKind.And))
        {
            SyntaxToken andToken = Previous;
            ExpressionSyntax right = ParseEqualityExpression();

            left = new BinaryExpression(
                left,
                andToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParseEqualityExpression()
    {
        ExpressionSyntax left = ParseComparisonExpression();

        while (Match(SyntaxTokenKind.EqualEqual, SyntaxTokenKind.BangEqual))
        {
            SyntaxToken operatorToken = Previous;
            ExpressionSyntax right = ParseComparisonExpression();

            left = new BinaryExpression(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParseComparisonExpression()
    {
        ExpressionSyntax left = ParseTermExpression();

        while (Match(
            SyntaxTokenKind.LessThan,
            SyntaxTokenKind.LessThanEqual,
            SyntaxTokenKind.GreaterThan,
            SyntaxTokenKind.GreaterThanEqual))
        {
            SyntaxToken operatorToken = Previous;
            ExpressionSyntax right = ParseTermExpression();

            left = new BinaryExpression(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParseTermExpression()
    {
        ExpressionSyntax left = ParseFactorExpression();

        while (Match(SyntaxTokenKind.Plus, SyntaxTokenKind.Minus))
        {
            SyntaxToken operatorToken = Previous;
            ExpressionSyntax right = ParseFactorExpression();

            left = new BinaryExpression(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParseFactorExpression()
    {
        ExpressionSyntax left = ParseUnaryExpression();

        while (Match(SyntaxTokenKind.Star, SyntaxTokenKind.Slash, SyntaxTokenKind.Percent))
        {
            SyntaxToken operatorToken = Previous;
            ExpressionSyntax right = ParseUnaryExpression();

            left = new BinaryExpression(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParseUnaryExpression()
    {
        if (Match(SyntaxTokenKind.Bang, SyntaxTokenKind.Minus, SyntaxTokenKind.Ampersand, SyntaxTokenKind.Star))
        {
            SyntaxToken operatorToken = Previous;
            ExpressionSyntax operand = ParseUnaryExpression();
            return new UnaryExpression(operatorToken, operand);
        }

        return ParsePrimaryExpression();
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        return Current.Kind switch
        {
            SyntaxTokenKind.Identifier => ParseIdentifierExpression(),
            SyntaxTokenKind.CharacterLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.StringLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.IntegerLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.FloatLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.True => ParseLiteralExpression(),
            SyntaxTokenKind.False => ParseLiteralExpression(),
            SyntaxTokenKind.LeftParen => ParseParenthesizedExpression(),
            _ => throw new ParseException($"Expected expression, but got {Current.Kind} at position {Current.Span.Start}")
        };
    }

    private ExpressionSyntax ParseIdentifierExpression()
    {
        SeparatedSyntax<TokenSyntax> identifierTokens = ParseSeparated(() => ParseToken(SyntaxTokenKind.Identifier), SyntaxTokenKind.Dot);
        return new IdentifierExpression(identifierTokens);
    }

    private LiteralExpression ParseLiteralExpression()
    {
        SyntaxToken literalToken = Expect(
            SyntaxTokenKind.CharacterLiteral,
            SyntaxTokenKind.StringLiteral,
            SyntaxTokenKind.IntegerLiteral,
            SyntaxTokenKind.FloatLiteral,
            SyntaxTokenKind.True,
            SyntaxTokenKind.False);
        return new LiteralExpression(literalToken);
    }

    private ParenthesizedExpression ParseParenthesizedExpression()
    {
        SyntaxToken leftParen = Expect(SyntaxTokenKind.LeftParen);
        ExpressionSyntax expression = ParseExpression();
        SyntaxToken rightParen = Expect(SyntaxTokenKind.RightParen);
        return new ParenthesizedExpression(leftParen, expression, rightParen);
    }
}

public sealed record UnaryExpression(
    SyntaxToken OperatorToken,
    ExpressionSyntax Operand
) : ExpressionSyntax;

public sealed record LiteralExpression(
    SyntaxToken LiteralToken
) : ExpressionSyntax;

public sealed record IdentifierExpression(
    SeparatedSyntax<TokenSyntax> IdentifierTokens
) : ExpressionSyntax;

public sealed record ParenthesizedExpression(
    SyntaxToken LeftParen,
    ExpressionSyntax Expression,
    SyntaxToken RightParen
) : ExpressionSyntax;
