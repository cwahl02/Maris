using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseImportDeclaration()
    {
        var importKeyword = Match(TokenKind.Import);
        var moduleName = ParseIdentifierPath();
        Token? asKeyword = null;
        Token? aliasIdentifier = null;
        Token semicolon;
        if (_iterator.Current.Kind == TokenKind.As)
        {
            asKeyword = Match(TokenKind.As);
            aliasIdentifier = Expect(TokenKind.Identifier);
        }
        semicolon = Match(TokenKind.Semicolon);

        return new ImportDeclarationSyntax(
            importKeyword,
            moduleName,
            semicolon
        );
    }

    private SyntaxNode ParseModuleDeclaration()
    {
        var moduleKeyword = Match(TokenKind.Module);
        var identifierPath = ParseIdentifierPath();
        if (_iterator.Current.Kind == TokenKind.LeftBrace)
        {
            var leftBrace = Match(TokenKind.LeftBrace);
            var body = ParseBlock();
            var rightBrace = Match(TokenKind.RightBrace);
            return new ModuleDeclarationSyntax(
                moduleKeyword,
                identifierPath,
                body
            );
        }
        throw new Exception($"Unexpected token: {_iterator.Current.Kind}");
    }
}
public abstract record FileItemSyntax : SyntaxNode;
public sealed record ImportDeclarationSyntax(
    Token ImportKeyword,
    SyntaxNode ModuleName, // IdentifierPathSyntax or StringLiteralSyntax
    Token? AsKeyword,
    Token? AliasIdentifier,
    Token Semicolon
) : FileItemSyntax;

public sealed record ModuleDeclarationSyntax(
    Token ModuleKeyword,
    IdentifierPathSyntax Name,
    BlockSyntax Body,
    Token RightBrace
) : FileItemSyntax;