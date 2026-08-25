using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ImportSyntax(
    SyntaxToken ImportKeyword,
    SyntaxNode Path,
    SyntaxToken? AsKeyword,
    SyntaxToken? Alias,
    SyntaxToken Semicolon
) : DeclarationSyntax;

public sealed partial class Parser
{
    private ImportSyntax ParseImportDeclaration()
    {
        SyntaxToken importKeyword = Expect(SyntaxTokenKind.Import);
        SyntaxNode path = ParseIdentifierPath();
        SyntaxToken? asKeyword = null;
        SyntaxToken? alias = null;

        if (_iterator.Current.Kind == SyntaxTokenKind.As)
        {
            asKeyword = Expect(SyntaxTokenKind.As);
            alias = Expect(SyntaxTokenKind.Identifier);
        }

        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new ImportSyntax(importKeyword, path, asKeyword, alias, semicolon);
    }
}