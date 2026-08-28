using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record BreakStatement(
    SyntaxToken BreakKeyword
) : StatementSyntax;

public sealed partial class Parser
{
    private BreakStatement ParseBreakStatement()
    {
        SyntaxToken breakKeyword = Expect(SyntaxTokenKind.Break);

        return new BreakStatement(
            breakKeyword
        );
    }
}