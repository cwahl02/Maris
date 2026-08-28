using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ReturnStatement(
    SyntaxToken ReturnKeyword,
    SeparatedSyntax<ExpressionSyntax>? Expressions
) : StatementSyntax;

public sealed partial class Parser
{
    private ReturnStatement ParseReturnStatement()
    {
        SyntaxToken returnKeyword = Expect(SyntaxTokenKind.Return);
        SeparatedSyntax<ExpressionSyntax>? expressions = null;
        if (Current.Kind != SyntaxTokenKind.Semicolon)
        {
            expressions = ParseSeparated(() => ParseExpression(), SyntaxTokenKind.Comma);
        }

        Expect(SyntaxTokenKind.Semicolon);

        return new ReturnStatement(
            returnKeyword,
            expressions
        );
    }
}