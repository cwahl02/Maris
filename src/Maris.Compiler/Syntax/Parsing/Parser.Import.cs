using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ImportDeclaration(
    SyntaxToken ImportKeyword,
    SeparatedSyntax<TokenSyntax> Path,
    SyntaxToken? AsKeyword,
    TokenSyntax? Alias,
    DeclarationAccessibility Accessibility = DeclarationAccessibility.Public
) : DeclarationSyntax(Accessibility);

public sealed partial class Parser
{
    private ImportDeclaration ParseImportDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        SyntaxToken importKeyword = Expect(SyntaxTokenKind.Import);
        SeparatedSyntax<TokenSyntax> path = ParseSeparated(() => ParseToken(SyntaxTokenKind.Identifier), SyntaxTokenKind.Dot);
        SyntaxToken? asKeyword = Match(SyntaxTokenKind.As) ? Expect(SyntaxTokenKind.As) : null;
        TokenSyntax? alias = asKeyword != null ? ParseToken(SyntaxTokenKind.Identifier) : null;

        Expect(SyntaxTokenKind.Semicolon);

        return new ImportDeclaration(
            importKeyword,
            path,
            asKeyword,
            alias,
            accessibility
        );
    }
}