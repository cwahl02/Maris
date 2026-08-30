using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private StatementSyntax ParseControlBody()
    {
        if (Match(SyntaxTokenKind.Colon))
        {
            return ParseStatement();
        }

        return ParseBlock();
    }
}
