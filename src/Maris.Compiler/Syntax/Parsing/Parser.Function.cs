using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ParameterGroup(
    SeparatedSyntax<TokenSyntax> Names,
    SyntaxToken Binding,
    TypeSyntax Type,
    SyntaxToken? EqualToken,
    ExpressionSyntax? Default
) : SyntaxNode;

public sealed record FunctionDeclaration(
    TokenSyntax Name,
    SyntaxToken ColonColon,
    SyntaxToken LeftParen,
    SeparatedSyntax<ParameterGroup>? Parameters,
    SyntaxToken RightParen,
    SyntaxToken? Arrow,
    SeparatedSyntax<TypeSyntax>? ReturnTypes,
    BlockSyntax Body,
    DeclarationAccessibility Accessibility = DeclarationAccessibility.Public
) : DeclarationSyntax(Accessibility);

public sealed partial class Parser
{
    // FunctionDeclaration := Identifier '::' '(' ParameterList ')' ReturnClause? Block
    private FunctionDeclaration ParseFunctionDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        TokenSyntax name = ParseToken(SyntaxTokenKind.Identifier);
        SyntaxToken colonColon = Expect(SyntaxTokenKind.ColonColon);
        SyntaxToken leftParen = Expect(SyntaxTokenKind.LeftParen);

        SeparatedSyntax<ParameterGroup>? parameters = Check(SyntaxTokenKind.RightParen)
            ? null
            : ParseSeparated(ParseParameterGroup, SyntaxTokenKind.Comma);

        SyntaxToken rightParen = Expect(SyntaxTokenKind.RightParen);

        SyntaxToken? arrow = Match(SyntaxTokenKind.Arrow) ? Previous : null;
        SeparatedSyntax<TypeSyntax>? returnTypes = arrow != null ? ParseSeparated(ParseType, SyntaxTokenKind.Comma) : null;

        BlockSyntax body = ParseBlock();

        return new FunctionDeclaration(
            name,
            colonColon,
            leftParen,
            parameters,
            rightParen,
            arrow,
            returnTypes,
            body,
            accessibility
        );
    }

    // ParameterGroup := IdentifierList (':' | '::') Type ('=' Literal)?
    private ParameterGroup ParseParameterGroup()
    {
        SeparatedSyntax<TokenSyntax> names = ParseSeparated(() => ParseToken(SyntaxTokenKind.Identifier), SyntaxTokenKind.Comma);
        SyntaxToken binding = Expect(SyntaxTokenKind.Colon, SyntaxTokenKind.ColonColon);
        TypeSyntax type = ParseType();

        SyntaxToken? equalToken = Match(SyntaxTokenKind.Equal) ? Previous : null;
        ExpressionSyntax? defaultValue = equalToken != null ? ParseLiteralExpression() : null;

        return new ParameterGroup(
            names,
            binding,
            type,
            equalToken,
            defaultValue
        );
    }
}
