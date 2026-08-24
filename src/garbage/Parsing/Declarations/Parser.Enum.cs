namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseEnumDeclaration()
    {
        var identifier = Expect(Lexing.TokenKind.Identifier);
        var colonColon = Match(Lexing.TokenKind.ColonColon);
        var enumKeyword = Match(Lexing.TokenKind.Enum);
        var body = ParseEnumBody();
        var semicolon = Match(Lexing.TokenKind.Semicolon);

        return new EnumDeclarationSyntax(
            identifier,
            colonColon,
            enumKeyword,
            body,
            semicolon
        );
    }

    private EnumBodySyntax ParseEnumBody()
    {
        var openBrace = Match(Lexing.TokenKind.LeftBrace);
        var members = new List<EnumMemberSyntax>();

        while (_iterator.Current.Kind != Lexing.TokenKind.RightBrace && !_iterator.IsAtEnd)
        {
            members.Add(ParseEnumMember());
        }

        var closeBrace = Match(Lexing.TokenKind.RightBrace);

        return new EnumBodySyntax(
            openBrace,
            members,
            closeBrace
        );
    }

    private EnumMemberSyntax ParseEnumMember()
    {
        var identifier = Match(Lexing.TokenKind.Identifier);
        Lexing.Token? equalsToken = null;
        ExpressionSyntax? value = null;

        if (_iterator.Current.Kind == Lexing.TokenKind.Equal)
        {
            equalsToken = Match(Lexing.TokenKind.Equal);
            value = ParseExpression();
        }

        return new EnumMemberSyntax(
            identifier,
            equalsToken,
            value
        );
    }
}

public sealed record EnumDeclarationSyntax(
    Lexing.Token Identifier,
    Lexing.Token ColonColon,
    Lexing.Token EnumKeyword,
    EnumBodySyntax Body,
    Lexing.Token Semicolon
) : DeclarationSyntax;

public sealed record EnumBodySyntax(
    Lexing.Token OpenBrace,
    List<EnumMemberSyntax> Members,
    Lexing.Token CloseBrace
) : SyntaxNode;

public sealed record EnumMemberSyntax(
    Lexing.Token Identifier,
    Lexing.Token? EqualsToken,
    ExpressionSyntax? Value
) : SyntaxNode;