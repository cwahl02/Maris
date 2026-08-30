using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ModuleDeclaration(
    SyntaxToken ModuleKeyword,
    SeparatedSyntax<TokenSyntax> Path,
    BlockSyntax? Body,
    DeclarationAccessibility Accessibility = DeclarationAccessibility.Public
) : DeclarationSyntax(Accessibility);

public sealed partial class Parser
{
    private ModuleDeclaration ParseModuleDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        SyntaxToken moduleKeyword = Expect(SyntaxTokenKind.Module);
        SeparatedSyntax<TokenSyntax> path = ParseSeparated(() => ParseToken(SyntaxTokenKind.Identifier), SyntaxTokenKind.Dot);

        BlockSyntax? body = null;
        if (Match(SyntaxTokenKind.LeftBrace))
        {
            body = ParseBlock();

            return new ModuleDeclaration(
                moduleKeyword,
                path,
                body,
                accessibility
            );
        } else {
            Expect(SyntaxTokenKind.Semicolon);
        }

        return new ModuleDeclaration(
            moduleKeyword,
            path,
            body,
            accessibility
        );
    }
}