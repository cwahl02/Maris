using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseDeclaration()
    {
        SyntaxTokenKind kind = _iterator.Current.Kind;
        return kind switch
        {
            SyntaxTokenKind.Identifier => ParseIdentifierLedDeclaration(),
            SyntaxTokenKind.Import or SyntaxTokenKind.Module => ParseKeywordDeclaration(),
            _ => throw new Exception($"Unexpected token: {kind}")
        };
    }
}