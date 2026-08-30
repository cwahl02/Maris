using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ContinueStatement(
    SyntaxToken ContinueKeyword
) : StatementSyntax;

public sealed partial class Parser
{
    private ContinueStatement ParseContinueStatement()
    {
        SyntaxToken continueKeyword = Expect(SyntaxTokenKind.Continue);

        return new ContinueStatement(
            continueKeyword
        );
    }
}