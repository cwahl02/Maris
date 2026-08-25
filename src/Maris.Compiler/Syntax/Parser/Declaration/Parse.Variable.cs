using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record VariableDeclarationSyntax(
    IdentifierListSyntax Identifiers,
    SyntaxToken Binding,
    TypeSyntax? Type,
    ExpressionListSyntax? Initializers
): DeclarationSyntax;

public sealed partial class Parser
{
    private VariableDeclarationSyntax ParseVariableDeclaration()
    {
        IdentifierListSyntax identifier = ParseIdentifierList();
        SyntaxToken binding = Expect(
            SyntaxTokenKind.Colon,
            SyntaxTokenKind.ColonColon,
            SyntaxTokenKind.ColonEqual,
            SyntaxTokenKind.ColonColonEqual
        );

        TypeSyntax? type = null;
        ExpressionListSyntax? initializers = null;
        if (binding.Kind == SyntaxTokenKind.ColonEqual ||
            binding.Kind == SyntaxTokenKind.ColonColonEqual)
        {
            initializers = ParseExpressionList();
        }
        else if (binding.Kind == SyntaxTokenKind.Colon || binding.Kind == SyntaxTokenKind.ColonColon)
        {
            type = ParseType();
        }
        else
        {
            throw new Exception($"Unexpected binding token kind: {binding.Kind}");
        }

        return new VariableDeclarationSyntax(
            identifier,
            binding,
            type,
            initializers
        );
    }
}