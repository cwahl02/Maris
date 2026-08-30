using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ContinueStatement(
    SyntaxToken ContinueKeyword,
    SyntaxToken Semicolon
) : StatementSyntax;

public sealed partial class Parser
{
    private ContinueStatement ParseContinueStatement()
    {
        SyntaxToken continueKeyword = Expect(SyntaxTokenKind.Continue);
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new ContinueStatement(
            continueKeyword,
            semicolon
        );
    }
}
