namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseIfStatement()
    {
        var ifKeyword = Match(Lexing.TokenKind.If);
        //var expression = ParseExpression();
        var controlBody = ParseControlBody();

        SyntaxNode? elseClause = null;
        if (_iterator.Current.Kind == Lexing.TokenKind.Else)
        {
            if (_iterator.Peek(1).Kind == Lexing.TokenKind.If)
            {
                elseClause = ParseIfStatement();
            }
            else
            {
                elseClause = ParseControlBody();
            }

                ifKeyword,
                //expression,
                controlBody,
                elseClause
            );
        }

        return new IfStatementSyntax(
            ifKeyword,
            //expression,
            controlBody,
            elseClause
        );
    }

    public sealed record IfStatementSyntax(
        Lexing.Token IfKeyword,
        //SyntaxNode Expression,
        SyntaxNode? ControlBody,
        SyntaxNode? ElseClause
    ) : SyntaxNode;
}