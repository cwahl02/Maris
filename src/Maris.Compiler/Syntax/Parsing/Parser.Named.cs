using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    // Dispatches on an identifier-led declaration: alias, function, or variable.
    private DeclarationSyntax ParseNamedDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        SyntaxTokenKind binding = Peek(1).Kind;

        if (binding == SyntaxTokenKind.ColonColon)
        {
            return Peek(2).Kind switch
            {
                SyntaxTokenKind.Alias => ParseAliasDeclaration(accessibility),
                SyntaxTokenKind.LeftParen => ParseFunctionDeclaration(accessibility),
                _ => ParseVariableDeclaration(accessibility)
            };
        }

        if (binding is SyntaxTokenKind.Colon or SyntaxTokenKind.ColonEqual or SyntaxTokenKind.ColonColonEqual)
        {
            return ParseVariableDeclaration(accessibility);
        }

        throw new ParseException($"Expected ':', '::', ':=' or '::=' after identifier, but got {binding} at position {Peek(1).Span.Start}");
    }
}
