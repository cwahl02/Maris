namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseAliasDeclaration()
    {
        var identifier = Match(Lexing.TokenKind.Identifier);
        var colonColon = Match(Lexing.TokenKind.ColonColon);
        var aliasKeyword = Match(Lexing.TokenKind.Alias);
        var type = ParseType();
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new AliasDeclarationSyntax(
            identifier,
            colonColon,
            aliasKeyword,
            type,
            semicolon
        );
    }
}

public sealed record AliasDeclarationSyntax(
    Lexing.Token Identifier,
    Lexing.Token ColonColon,
    Lexing.Token AliasKeyword,
    TypeSyntax Type,
    Lexing.Token Semicolon
) : DeclarationSyntax;