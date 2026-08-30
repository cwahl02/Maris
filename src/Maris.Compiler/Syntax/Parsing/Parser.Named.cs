using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    // Dispatches on an identifier-led declaration: alias, function, or variable.
    // Alias and function declarations always start with a single identifier
    // (Identifier '::' ...); a comma-separated identifier list can only lead a
    // VariableDeclaration.
    private DeclarationSyntax ParseNamedDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        int offset = 1;
        while (Peek(offset).Kind == SyntaxTokenKind.Comma && Peek(offset + 1).Kind == SyntaxTokenKind.Identifier)
        {
            offset += 2;
        }

        bool singleName = offset == 1;
        SyntaxTokenKind binding = Peek(offset).Kind;

        if (singleName && binding == SyntaxTokenKind.ColonColon)
        {
            return Peek(offset + 1).Kind switch
            {
                SyntaxTokenKind.Alias => ParseAliasDeclaration(accessibility),
                SyntaxTokenKind.LeftParen => ParseFunctionDeclaration(accessibility),
                _ => ParseVariableDeclaration(accessibility)
            };
        }

        if (binding is SyntaxTokenKind.Colon or SyntaxTokenKind.ColonColon or SyntaxTokenKind.ColonEqual or SyntaxTokenKind.ColonColonEqual)
        {
            return ParseVariableDeclaration(accessibility);
        }

        throw new ParseException($"Expected ':', '::', ':=' or '::=' after identifier, but got {binding} at position {Peek(offset).Span.Start}");
    }
}
