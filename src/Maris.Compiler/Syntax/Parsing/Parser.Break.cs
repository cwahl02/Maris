using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record BreakStatement(
    SyntaxToken BreakKeyword,
    SyntaxToken Semicolon
) : StatementSyntax;

public sealed partial class Parser
{
    private BreakStatement ParseBreakStatement()
    {
        SyntaxToken breakKeyword = Expect(SyntaxTokenKind.Break);
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new BreakStatement(
            breakKeyword,
            semicolon
        );
    }
}
