using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record NameListSyntax(
    List<SyntaxToken> Names
) : DeclarationSyntax;

public sealed partial class Parser
{
    private NameListSyntax ParseNameList()
    {
        List<SyntaxToken> names = new();

        while (_iterator.Current.Kind == SyntaxTokenKind.Identifier)
        {
            names.Add(_iterator.Current);
            _iterator.Forward();
            if (_iterator.Current.Kind == SyntaxTokenKind.Comma)
            {
                _iterator.Forward();
            }
        }
        
        return new NameListSyntax(names);
    }
}