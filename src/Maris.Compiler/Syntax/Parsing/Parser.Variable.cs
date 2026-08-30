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

        TypeSyntax? type = Check(SyntaxTokenKind.Equal, SyntaxTokenKind.Semicolon) ? null : ParseType();

        SyntaxToken? equalToken = Match(SyntaxTokenKind.Equal) ? Previous : null;
        ExpressionSyntax? initializer = equalToken != null ? ParseExpression() : null;

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
