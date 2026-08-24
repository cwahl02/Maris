using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record IdentifierPathSyntax(List<SyntaxToken> Identifiers) : ExpressionSyntax;

public sealed partial class Parser
{
    private IdentifierPathSyntax ParseIdentifierPath()
    {
        SyntaxToken identifier = Expect(SyntaxTokenKind.Identifier);
        List<SyntaxToken> identifiers = new() { identifier };

        while (_iterator.Current.Kind == SyntaxTokenKind.Dot)
        {
            Expect(SyntaxTokenKind.Dot);
            identifiers.Add(Expect(SyntaxTokenKind.Identifier));
        }

        return new IdentifierPathSyntax(identifiers);
    }
}