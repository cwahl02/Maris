using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record LiteralExpressionSyntax(
    SyntaxToken LiteralToken
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParsePrimaryExpression()
    {
        SyntaxTokenKind kind = _iterator.Current.Kind;
        
        return kind switch
        {
            SyntaxTokenKind.Identifier => ParseIdentifierExpression(),
            SyntaxTokenKind.CharacterLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.StringLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.IntegerLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.FloatLiteral => ParseLiteralExpression(),
            SyntaxTokenKind.True => ParseLiteralExpression(),
            SyntaxTokenKind.False => ParseLiteralExpression(),
            SyntaxTokenKind.Null => ParseLiteralExpression(),
            SyntaxTokenKind.LeftParen => ParseGroupExpression(),
            SyntaxTokenKind.LeftBrace => ParseInitializerExpression(),
            _ => throw new Exception($"Expected expression, found {kind}.")
        };
    }
}