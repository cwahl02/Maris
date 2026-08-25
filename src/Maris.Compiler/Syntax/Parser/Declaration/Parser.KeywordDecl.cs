using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseKeywordDeclaration()
    {
        return _iterator.Current.Kind switch
        {
            SyntaxTokenKind.Import => ParseImportDeclaration(),
            SyntaxTokenKind.Module => ParseModuleDeclaration(),
            _ => throw new Exception($"Unexpected token: {_iterator.Current.Kind}")
        };
    }
}