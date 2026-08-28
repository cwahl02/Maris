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

        StatementSyntax then;
        if (Current.Kind == SyntaxTokenKind.Colon)
        {
            Expect(SyntaxTokenKind.Colon);
            then = ParseStatement();
        }
        else if (Current.Kind == SyntaxTokenKind.LeftBrace)
        {
            then = ParseBlock();
        }
        else
        {
            throw new Exception("Expected ':' or '{' after 'if' condition.");
        }

        SyntaxToken? elseKeyword = null;
        StatementSyntax? elseStatement = null;
        if (Current.Kind == SyntaxTokenKind.Else && Peek(1).Kind == SyntaxTokenKind.If)
        {
            elseKeyword = Expect(SyntaxTokenKind.Else);
            elseStatement = ParseIfStatement();
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