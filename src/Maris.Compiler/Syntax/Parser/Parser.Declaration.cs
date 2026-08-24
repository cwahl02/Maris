using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseDeclaration()
    {
        return _iterator.Current.Kind switch
        {
            SyntaxTokenKind.Import => ParseImport(),
            SyntaxTokenKind.Module => ParseModule(),
            _ => throw new Exception($"Unexpected token of kind {_iterator.Current.Kind} at position {_iterator.Position}."),
        };
    }
}