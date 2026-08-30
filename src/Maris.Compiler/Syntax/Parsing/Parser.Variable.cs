using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record VariableDeclaration(
    SeparatedSyntax<TokenSyntax> Names,
    SyntaxToken Binding,
    TypeSyntax? Type,
    SyntaxToken? EqualToken,
    ExpressionSyntax? Initializer,
    SyntaxToken Semicolon,
    DeclarationAccessibility Accessibility = DeclarationAccessibility.Public
) : DeclarationSyntax(Accessibility);

public sealed partial class Parser
{
    // VariableDeclaration := IdentifierList Binding Type? ('=' Expression)? ';'
    // Binding              := ':' | '::' | ':=' | '::='
    // Note: ':=' and '::=' already imply an initializer, so no explicit type or
    // '=' token follows them; the expression is parsed directly.
    private VariableDeclaration ParseVariableDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        SeparatedSyntax<TokenSyntax> names = ParseSeparated(() => ParseToken(SyntaxTokenKind.Identifier), SyntaxTokenKind.Comma);
        SyntaxToken binding = Expect(
            SyntaxTokenKind.Colon,
            SyntaxTokenKind.ColonColon,
            SyntaxTokenKind.ColonEqual,
            SyntaxTokenKind.ColonColonEqual);

        TypeSyntax? type = null;
        SyntaxToken? equalToken = null;
        ExpressionSyntax? initializer = null;

        if (binding.Kind is SyntaxTokenKind.ColonEqual or SyntaxTokenKind.ColonColonEqual)
        {
            initializer = ParseExpression();
        }
        else
        {
            type = Check(SyntaxTokenKind.Equal, SyntaxTokenKind.Semicolon) ? null : ParseType();
            equalToken = Match(SyntaxTokenKind.Equal) ? Previous : null;
            initializer = equalToken != null ? ParseExpression() : null;
        }

        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new VariableDeclaration(
            names,
            binding,
            type,
            equalToken,
            initializer,
            semicolon,
            accessibility
        );
    }
}
