using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ModuleSyntax(
    SyntaxToken ModuleKeyword,
    IdentifierPathSyntax Path,
    DeclarationSyntax? Body,
    SyntaxToken? Semicolon
) : DeclarationSyntax;

public sealed partial class Parser
{
    private ModuleSyntax ParseModuleDeclaration()
    {
        SyntaxToken moduleKeyword = Expect(SyntaxTokenKind.Module);
        IdentifierPathSyntax path = ParseIdentifierPath();
        DeclarationSyntax? body = null;

        if (_iterator.Current.Kind == SyntaxTokenKind.LeftBrace)
        {
            body = ParseBlock();
        }

        SyntaxToken? semicolon = null;
        if (_iterator.Current.Kind == SyntaxTokenKind.Semicolon)
        {
            semicolon = Expect(SyntaxTokenKind.Semicolon);
        }

        return new ModuleSyntax(moduleKeyword, path, body, semicolon);
    }
}