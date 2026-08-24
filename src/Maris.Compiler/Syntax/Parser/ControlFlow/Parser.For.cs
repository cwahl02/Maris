using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ForSyntax(
    SyntaxToken ForKeyword,
    ExpressionSyntax? Initializer,
    ExpressionSyntax? Condition,
    ExpressionSyntax? Increment,
    StatementSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    private ForSyntax ParseFor()
    {
        SyntaxToken forKeyword = Expect(SyntaxTokenKind.For);
        ExpressionSyntax? initializer = null;
        ExpressionSyntax? condition = null;
        ExpressionSyntax? increment = null;

        if (_iterator.Current.Kind != SyntaxTokenKind.Semicolon)
        {
            initializer = ParseExpression();
        }

        Expect(SyntaxTokenKind.Semicolon);

        if (_iterator.Current.Kind != SyntaxTokenKind.Semicolon)
        {
            condition = ParseExpression();
        }

        Expect(SyntaxTokenKind.Semicolon);

        if (_iterator.Current.Kind != SyntaxTokenKind.LeftBrace)
        {
            increment = ParseExpression();
        }

        StatementSyntax body = ParseControlFlowBody();

        return new ForSyntax(forKeyword, initializer, condition, increment, body);
    }
}