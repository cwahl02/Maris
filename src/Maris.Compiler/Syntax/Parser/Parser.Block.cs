using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record BlockSyntax(
    SyntaxToken LeftBrace,
    List<StatementSyntax> Statements,
    SyntaxToken RightBrace
) : DeclarationSyntax;

public sealed partial class Parser
{
    private BlockSyntax ParseBlock()
    {
        SyntaxToken leftBrace = Expect(SyntaxTokenKind.LeftBrace);
        List<StatementSyntax> statements = new();

        while (_iterator.Current.Kind != SyntaxTokenKind.RightBrace)
        {
            statements.Add(ParseStatement());
        }

        SyntaxToken rightBrace = Expect(SyntaxTokenKind.RightBrace);

        return new BlockSyntax(leftBrace, statements, rightBrace);
    }
}