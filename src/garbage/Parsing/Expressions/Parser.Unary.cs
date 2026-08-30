namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseUnaryExpression()
    {
        if (_iterator.Current.Kind is Lexing.TokenKind.Plus or Lexing.TokenKind.Minus or Lexing.TokenKind.Bang or Lexing.TokenKind.Tilde or Lexing.TokenKind.Star)
        {
            var operatorToken = _iterator.Current;
            _iterator.Forward();
            var operand = ParseUnaryExpression();

            return new UnaryExpressionSyntax(
                operatorToken,
                operand
            );
        }

        return ParsePostfixExpression();
    }
}