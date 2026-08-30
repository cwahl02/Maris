using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record IfStatement(
    SyntaxToken IfKeyword,
    ExpressionSyntax Condition,
    StatementSyntax Then,
    SyntaxToken? ElseKeyword,
    StatementSyntax? ElseStatement
) : StatementSyntax;

public sealed partial class Parser
{
    private IfStatement ParseIfStatement()
    {
        SyntaxToken ifKeyword = Expect(SyntaxTokenKind.If);
        ExpressionSyntax condition = ParseExpression();
        StatementSyntax then = ParseControlBody();

        SyntaxToken? elseKeyword = null;
        StatementSyntax? elseStatement = null;
        if (Match(SyntaxTokenKind.Else))
        {
            elseKeyword = Previous;
            elseStatement = Current.Kind == SyntaxTokenKind.If
                ? ParseIfStatement()
                : ParseControlBody();
        }


        return new IfStatement(
            ifKeyword,
            condition,
            then,
            elseKeyword,
            elseStatement
        );
    }
}