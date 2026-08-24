using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record IfSyntax(
    SyntaxToken IfKeyword,
    ExpressionSyntax Condition,
    StatementSyntax Then,
    SyntaxToken? ElseKeyword,
    IfSyntax? Else
) : StatementSyntax;

public sealed partial class Parser
{
    private IfSyntax ParseIf()
    {
        SyntaxToken ifKeyword = Expect(SyntaxTokenKind.If);
        ExpressionSyntax condition = ParseExpression();
        StatementSyntax then = ParseControlFlowBody();
        SyntaxToken? elseKeyword = null;
        IfSyntax? @else = null;

        if (_iterator.Current.Kind == SyntaxTokenKind.Else)
        {
            elseKeyword = Expect(SyntaxTokenKind.Else);
            @else = ParseIf();
        }

        return new IfSyntax(ifKeyword, condition, then, elseKeyword, @else);
    }
}