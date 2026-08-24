using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record FunctionSyntax(
    SyntaxToken Identifier,
    ParameterListSyntax Parameters,
    SyntaxToken ColonColon,
    ReturnClauseSyntax? ReturnClause,
    StatementSyntax? Body
) : DeclarationSyntax;

public sealed record ParameterListSyntax(
    SyntaxToken LeftParen,
    IReadOnlyList<ParameterGroupSyntax> Parameters,
    SyntaxToken RightParen
) : SyntaxNode;

public sealed record ParameterGroupSyntax(
    IdentifierListSyntax Identifiers,
    TypeSyntax Type
) : SyntaxNode;

public sealed partial class Parser
{
    private DeclarationSyntax ParseFunction()
    {
        return _iterator.Current.Kind switch
        {
            SyntaxTokenKind.Import => ParseImport(),
            SyntaxTokenKind.Module => ParseModule(),
            _ => throw new Exception($"Unexpected token of kind {_iterator.Current.Kind} at position {_iterator.Position}."),
        };
    }
}