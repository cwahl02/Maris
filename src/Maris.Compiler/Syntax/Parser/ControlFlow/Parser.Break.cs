using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record BreakSyntax(
    SyntaxToken BreakKeyword,
    SyntaxToken Semicolon
) : StatementSyntax;

public sealed partial class Parser
{
    private BreakSyntax ParseBreak()
    {
        SyntaxToken breakKeyword = Expect(SyntaxTokenKind.Break);
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new BreakSyntax(breakKeyword, semicolon);
    }
}