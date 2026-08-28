using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record DeclarationStatement(
    DeclarationSyntax Declaration,
    SyntaxToken? Semicolon
) : StatementSyntax;

public sealed partial class Parser
{
    private DeclarationSyntax ParseDeclaration(DeclarationAccessibility accessibility)
    {
        switch (Current.Kind)
        {
            case SyntaxTokenKind.Module:
                return ParseModuleDeclaration(accessibility);
            case SyntaxTokenKind.Import:
                return ParseImportDeclaration(accessibility);
            
            case SyntaxTokenKind.Identifier:
                return ParseNamedDeclaration(accessibility);
            default:
                throw new Exception($"Expected declaration, but got {Current.Kind} at position {Current.Span.Start}");
        } 
    }

    private DeclarationStatement ParseDeclarationStatement()
    {
        DeclarationAccessibility accessibility = ParseDeclarationAccessibility();
        DeclarationSyntax declaration = ParseDeclaration(accessibility);
        SyntaxToken? semicolon = Match(SyntaxTokenKind.Semicolon) ? Expect(SyntaxTokenKind.Semicolon) : null;

        return new DeclarationStatement(declaration, semicolon);
    }

    private DeclarationAccessibility ParseDeclarationAccessibility()
    {
        switch (Current.Kind)
        {
            case SyntaxTokenKind.Plus:
                Forward();
                return DeclarationAccessibility.Public;
            case SyntaxTokenKind.Minus:
                Forward();
                return DeclarationAccessibility.Private;
            default:
                return DeclarationAccessibility.Public;
        }
    }
}