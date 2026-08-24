using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ContinueSyntax(
    SyntaxToken ContinueKeyword,
    SyntaxToken Semicolon
) : StatementSyntax;

public sealed partial class Parser
{
    private ContinueSyntax ParseContinue()
    {
        SyntaxToken continueKeyword = Expect(SyntaxTokenKind.Continue);
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new ContinueSyntax(continueKeyword, semicolon);
    }
}