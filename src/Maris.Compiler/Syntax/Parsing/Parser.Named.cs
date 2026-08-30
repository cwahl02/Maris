using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseNamedDeclaration(
        DeclarationAccessibility accessibility
    )
    {
        SyntaxTokenKind binding = Peek(1).Kind;
        if(binding == SyntaxTokenKind.ColonColon)
        {
            SyntaxTokenKind keyword = Peek(2).Kind;
            return keyword switch
            {
                SyntaxTokenKind.Alias => ParseAliasDeclaration(accessibility),
                _ => throw new Exception($"Expected 'class', 'enum', 'interface', or 'struct', but got {keyword} at position {Peek(2).Span.Start}")
            };
        }

        throw new Exception($"Expected '::' after identifier, but got {binding} at position {Peek(1).Span.Start}");
    }
}