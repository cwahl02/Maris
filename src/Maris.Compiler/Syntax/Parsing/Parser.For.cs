using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ForStatement(
    SyntaxToken ForKeyword,
    ExpressionSyntax? Initializer,
    SyntaxToken FirstSemicolon,
    ExpressionSyntax? Condition,
    SyntaxToken SecondSemicolon,
    ExpressionSyntax? Iteration,
    StatementSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    // ForStatement := 'for' ForClause ControlBody
    // ForClause    := ForInitializer? ';' ForCondition? ';' ForIteration?
    private ForStatement ParseForStatement()
    {
        SyntaxToken forKeyword = Expect(SyntaxTokenKind.For);

        ExpressionSyntax? initializer = Check(SyntaxTokenKind.Semicolon) ? null : ParseExpression();
        SyntaxToken firstSemicolon = Expect(SyntaxTokenKind.Semicolon);

        ExpressionSyntax? condition = Check(SyntaxTokenKind.Semicolon) ? null : ParseExpression();
        SyntaxToken secondSemicolon = Expect(SyntaxTokenKind.Semicolon);

        ExpressionSyntax? iteration = Check(SyntaxTokenKind.LeftBrace, SyntaxTokenKind.Colon) ? null : ParseExpression();

        StatementSyntax body = ParseControlBody();

        return new ForStatement(
            forKeyword,
            initializer,
            firstSemicolon,
            condition,
            secondSemicolon,
            iteration,
            body
        );
    }
}
