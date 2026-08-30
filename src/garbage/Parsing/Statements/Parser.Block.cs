namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseBlockStatement()
    {
        var leftBrace = Match(Lexing.TokenKind.LeftBrace);
        var statements = new List<SyntaxNode>();
        while (_iterator.Current.Kind != Lexing.TokenKind.RightBrace)
        {
            statements.Add(ParseStatement());
        }
        var rightBrace = Match(Lexing.TokenKind.RightBrace);

        return new BlockStatementSyntax(
            leftBrace,
            statements,
            rightBrace
        );
    }
}