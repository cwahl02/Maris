using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record LiteralExpressionSyntax(
    SyntaxToken LiteralToken
) : ExpressionSyntax;

public sealed partial class Parser
{
    private ExpressionSyntax ParsePrimary()
    {
        if (_iterator.Current.Kind == SyntaxTokenKind.Identifier)
        {
            return ParseIdentifierPath();
        }

        if (_iterator.Current.Kind == SyntaxTokenKind.CharacterLiteral ||
            _iterator.Current.Kind == SyntaxTokenKind.StringLiteral ||
            _iterator.Current.Kind == SyntaxTokenKind.IntegerLiteral ||
            _iterator.Current.Kind == SyntaxTokenKind.FloatLiteral ||
            _iterator.Current.Kind == SyntaxTokenKind.True ||
            _iterator.Current.Kind == SyntaxTokenKind.False ||
            _iterator.Current.Kind == SyntaxTokenKind.Null)
        {
            var literalToken = _iterator.Current;
            _iterator.Forward();
            return new LiteralExpressionSyntax(literalToken);
        }

        if (_iterator.Current.Kind == SyntaxTokenKind.LeftParen)
        {
            _iterator.Forward();
            var expr = ParseExpression();
            Expect(SyntaxTokenKind.RightParen);
            return new GroupExpressionSyntax(expr);
        }

        throw new Exception($"Expected expression, found {_iterator.Current.Kind}.");
    }
}