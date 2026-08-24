using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record DeferSyntax(
    SyntaxToken DeferKeyword,
    StatementSyntax Statement,
    SyntaxToken Semicolon
) : DeclarationSyntax;

public sealed partial class Parser
{
    private DeferSyntax ParseDefer()
    {
        SyntaxToken deferKeyword = Expect(SyntaxTokenKind.Defer);
        StatementSyntax body;
        if(_iterator.Current.Kind == SyntaxTokenKind.LeftBrace)
        {
            body = ParseBlock();
        }
        else
        {
            body = ParseStatement();
        }
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new DeferSyntax(deferKeyword, body, semicolon);
    }
}