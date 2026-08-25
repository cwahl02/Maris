using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseControl()
    {
        return _iterator.Current.Kind switch
        {
            SyntaxTokenKind.If => ParseIf(),
            SyntaxTokenKind.While => ParseWhile(),
            SyntaxTokenKind.For => ParseFor(),
            SyntaxTokenKind.Switch => ParseSwitch(),
            SyntaxTokenKind.Return => ParseReturn(),
            SyntaxTokenKind.Break => ParseBreak(),
            SyntaxTokenKind.Continue => ParseContinue(),
            SyntaxTokenKind.Defer => ParseDefer(),
            // SyntaxTokenKind.Yield => ParseYield(),
            _ => throw new Exception($"Unexpected token of kind {_iterator.Current.Kind} at position {_iterator.Position}."),
        };
    }
}