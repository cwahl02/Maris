using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record AliasDeclaration(
    TokenSyntax Name,
    SyntaxToken ColonColon,
    SyntaxToken AliasKeyword,
    TypeSyntax Type,
    SyntaxToken Semicolon,
    DeclarationAccessibility Accessibility = DeclarationAccessibility.Public
) : DeclarationSyntax(Accessibility);

public sealed partial class Parser
{
    // AliasDeclaration := Identifier '::' 'alias' Type ';'
    private AliasDeclaration ParseAliasDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        TokenSyntax name = ParseToken(SyntaxTokenKind.Identifier);
        SyntaxToken colonColon = Expect(SyntaxTokenKind.ColonColon);
        SyntaxToken aliasKeyword = Expect(SyntaxTokenKind.Alias);
        TypeSyntax type = ParseType();
        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new AliasDeclaration(
            name,
            colonColon,
            aliasKeyword,
            type,
            semicolon,
            accessibility
        );
    }
}
