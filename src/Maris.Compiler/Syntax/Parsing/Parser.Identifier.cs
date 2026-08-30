using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record TokenSyntax(
    SyntaxToken Token
) : SyntaxNode;

public sealed partial class Parser
{
    private TokenSyntax ParseToken(SyntaxTokenKind kind)
    {
        SyntaxToken token = Expect(kind);

        return new TokenSyntax(
            token
        );
    }
}