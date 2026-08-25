using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseIdentifierLedDeclaration()
    {
        SyntaxTokenKind binding = _iterator.Peek(1).Kind;
        if (binding == SyntaxTokenKind.ColonColon)
        {
            SyntaxTokenKind keywordKind = _iterator.Peek(2).Kind;

            switch (keywordKind)
            {
                case SyntaxTokenKind.Alias:
                    return ParseAliasDeclaration();
                case SyntaxTokenKind.Distinct:
                    return ParseDistinctDeclaration();
                case SyntaxTokenKind.Enum:
                    return ParseEnumDeclaration();
                case SyntaxTokenKind.Struct:
                    return ParseStructDeclaration();
                case SyntaxTokenKind.Union:
                    return ParseUnionDeclaration();
                case SyntaxTokenKind.LeftParen:
                    return ParseFunctionDeclaration();
                default:
                    break;
            }
        }

        if (binding == SyntaxTokenKind.Colon ||
                 binding == SyntaxTokenKind.ColonColon ||
                 binding == SyntaxTokenKind.ColonEqual ||
                 binding == SyntaxTokenKind.ColonColonEqual)
        {
            return ParseVariableDeclaration();
        }

        throw new Exception($"Unexpected token: {binding}");
    }
}