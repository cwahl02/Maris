using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ExpressionStatement(
    SeparatedSyntax<ExpressionSyntax> Expressions
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

        return new ExpressionStatement(
            expressions
        );
    }

    private ExpressionSyntax ParseExpression()
    {
        return ParseAssignmentExpression();
    }

    private ExpressionSyntax ParseAssignmentExpression()
    {
        ExpressionSyntax left = ParseLogicalOrExpression();
        
        if (!Match(SyntaxTokenKind.Equal))
        {
            return left;
        }

        SyntaxToken equalToken = Expect(SyntaxTokenKind.Equal);
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

        if (!Match(SyntaxTokenKind.Or))
        {
            return left;
        }

        SyntaxToken orToken = Expect(SyntaxTokenKind.Or);
        ExpressionSyntax right = ParseLogicalOrExpression();

        return new BinaryExpression(
            left,
            orToken,
            right
        );
    }

    private ExpressionSyntax ParseLogicalAndExpression()
    {
        ExpressionSyntax left = ParseEqualityExpression();

        if (!Match(SyntaxTokenKind.And))
        {
            return left;
        }

        SyntaxToken andToken = Expect(SyntaxTokenKind.And);
        ExpressionSyntax right = ParseLogicalAndExpression();

        return new BinaryExpression(
            left,
            andToken,
            right
        );
    }

    private ExpressionSyntax ParseEqualityExpression()
    {
        ExpressionSyntax left = ParseComparisonExpression();

        if (!Match(SyntaxTokenKind.EqualEqual, SyntaxTokenKind.BangEqual))
        {
            return left;
        }
    
        SyntaxToken operatorToken = Expect(SyntaxTokenKind.EqualEqual, SyntaxTokenKind.BangEqual);
        
        ExpressionSyntax right = ParseComparisonExpression();

        return new BinaryExpression(
            left,
            operatorToken,
            right
        );
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
            SyntaxToken operatorToken = Expect(
                SyntaxTokenKind.LessThan,
                SyntaxTokenKind.LessThanEqual,
                SyntaxTokenKind.GreaterThan,
                SyntaxTokenKind.GreaterThanEqual);

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
        Console.WriteLine($"Term: {Current.Kind}");

        ExpressionSyntax left = ParseFactorExpression();

        while (Match(SyntaxTokenKind.Plus, SyntaxTokenKind.Minus))
        {
            SyntaxToken operatorToken = Expect(SyntaxTokenKind.Plus, SyntaxTokenKind.Minus);
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
        Console.WriteLine($"Factor: {Current.Kind}");

        ExpressionSyntax left = ParsePrimaryExpression();

        while (Match(SyntaxTokenKind.Star, SyntaxTokenKind.Slash, SyntaxTokenKind.Percent))
        {
            SyntaxToken operatorToken = Expect(SyntaxTokenKind.Star, SyntaxTokenKind.Slash, SyntaxTokenKind.Percent);
            ExpressionSyntax right = ParsePrimaryExpression();

            left = new BinaryExpression(
                left,
                operatorToken,
                right
            );
        }

        return left;
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        Console.WriteLine($"Primary: {Current.Kind}");

        return Current.Kind switch
        {
            SyntaxTokenKind.Identifier => ParseIdentifierExpression(),
            SyntaxTokenKind.StringLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.IntegerLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.FloatLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.True => ParseLiteralExpression(),
            SyntaxTokenKind.False => ParseLiteralExpression(),
            SyntaxTokenKind.LeftParen => ParseParenthesizedExpression(),
            _ => throw new Exception($"Expected expression, but got {Current.Kind} at position {Current.Span.Start}")
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