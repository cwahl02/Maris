using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record SeparatedSyntax<T>(
    T[] Elements,
    SyntaxTokenKind Separator
) : SyntaxNode;

public sealed partial class Parser
{
    private SeparatedSyntax<T> ParseSeparated<T>(
        Func<T> parseElement,
        SyntaxTokenKind separator
    )
    {
        List<T> elements = new List<T>();
        while (true)
        {
            elements.Add(parseElement());
            if (!Match(separator))
            {
                break;
            }
        }

        return new SeparatedSyntax<T>(
            elements.ToArray(),
            separator
        );
    }
}