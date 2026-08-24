using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseStructDeclaration()
    {
        var identifier = Expect(TokenKind.Identifier);
        var colonColon = Match(TokenKind.ColonColon);
        var structKeyword = Match(TokenKind.Struct);
        var body = ParseStructBody();
        var semicolon = Match(TokenKind.Semicolon);

        return new StructDeclarationSyntax(
            identifier,
            colonColon,
            structKeyword,
            body,
            semicolon
        );
    }

    private StructBodySyntax ParseStructBody()
    {
        var openBrace = Match(TokenKind.LeftBrace);
        var members = new List<StructMemberSyntax>();

        while (_iterator.Current.Kind != TokenKind.RightBrace && !_iterator.IsAtEnd)
        {
            members.Add(ParseEnumMember());
        }

        var closeBrace = Match(TokenKind.RightBrace);

        return new StructBodySyntax(
            openBrace,
            members,
            closeBrace
        );
    }

    private EnumMemberSyntax ParseStructMember()
    {
        var identifier = Match(TokenKind.Identifier);
        Token? equalsToken = null;
        ExpressionSyntax? value = null;

        if (_iterator.Current.Kind == TokenKind.Equal)
        {
            equalsToken = Match(TokenKind.Equal);
            value = ParseExpression();
        }

        return new EnumMemberSyntax(
            identifier,
            equalsToken,
            value
        );
    }
}

public sealed record StructDeclarationSyntax(
    Token Identifier,
    Token ColonColon,
    Token StructKeyword,
    StructBodySyntax Body,
    Token Semicolon
) : DeclarationSyntax;

public sealed record StructBodySyntax(
    Token LeftBrace,
    List<StructMemberSyntax> Members,
    Token RightBrace
) : SyntaxNode;

public sealed record StructMemberSyntax(
    IdentifierListSyntax Identifiers,
    Token? EqualToken,
    ExpressionSyntax? Value
) : SyntaxNode;