namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseDistinctDeclaration()
    {
        var identifier = Match(Lexing.TokenKind.Identifier);
        var colonColon = Match(Lexing.TokenKind.ColonColon);
        var distinctKeyword = Match(Lexing.TokenKind.Distinct);
        var type = ParseType();
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new DistinctDeclarationSyntax(
            identifier,
            colonColon,
            distinctKeyword,
            type,
            semicolon
        );
    }
}

public sealed record DistinctDeclarationSyntax(
    Lexing.Token Identifier,
    Lexing.Token ColonColon,
    Lexing.Token DistinctKeyword,
    TypeSyntax Type,
    Lexing.Token Semicolon
) : DeclarationSyntax;