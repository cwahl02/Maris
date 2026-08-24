using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseDeclaration()
    {
        if (_iterator.Current.Kind == TokenKind.Module)
        {
            return ParseModuleDeclaration();
        }

        if (_iterator.Current.Kind == TokenKind.Import)
        {
            return ParseImportDeclaration();
        }

        if (MatchSequence(TokenKind.Identifier, TokenKind.ColonColon, TokenKind.Alias))
        {
            return ParseAliasDeclaration();
        }

        if (MatchSequence(TokenKind.Identifier, TokenKind.ColonColon, TokenKind.Distinct))
        {
            return ParseDistinctDeclaration();
        }

        if (MatchSequence(TokenKind.Identifier, TokenKind.ColonColon, TokenKind.Enum))
        {
            return ParseEnumDeclaration();
        }

        if (MatchSequence(TokenKind.Identifier, TokenKind.ColonColon, TokenKind.Struct))
        {
            return ParseStructDeclaration();
        }

        if (MatchSequence(TokenKind.Identifier, TokenKind.ColonColon, TokenKind.Union))
        {
            return ParseUnionDeclaration();
        }
    }
}