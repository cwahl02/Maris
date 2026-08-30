using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record AliasDeclaration(
    TokenSyntax Name,
    SyntaxToken ColonColon,
    SyntaxToken AliasKeyword,
    SyntaxNode? Type,
    SyntaxToken? AsKeyword,
    TokenSyntax? Alias,
    DeclarationAccessibility Accessibility = DeclarationAccessibility.Public
) : DeclarationSyntax(Accessibility);

public sealed partial class Parser
{
    private AliasDeclaration ParseAliasDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        SyntaxToken aliasKeyword = Expect(SyntaxTokenKind.Alias);
        TokenSyntax identifier = ParseToken(SyntaxTokenKind.Identifier);
        SyntaxToken? asKeyword = Match(SyntaxTokenKind.As) ? Expect(SyntaxTokenKind.As) : null;
        TokenSyntax? alias = asKeyword != null ? ParseToken(SyntaxTokenKind.Identifier) : null;

        return new AliasDeclaration(
            identifier,
            Expect(SyntaxTokenKind.ColonColon),
            aliasKeyword,
            null,
            asKeyword,
            alias,
            accessibility
        );
    }
}