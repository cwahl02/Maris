using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private StatementSyntax ParseStatement()
    {
        return _iterator.Current.Kind switch
        {
            SyntaxTokenKind.Identifier => ParseExpression(),
            SyntaxTokenKind.If => ParseIf(),
            SyntaxTokenKind.While => ParseWhile(),
            SyntaxTokenKind.For => ParseFor(),
            SyntaxTokenKind.Return => ParseReturn(),
            SyntaxTokenKind.Break => ParseBreak(),
            SyntaxTokenKind.Continue => ParseContinue(),
            SyntaxTokenKind.LeftBrace => ParseBlock(),
            SyntaxTokenKind.Defer => ParseDefer(),
            _ => throw new Exception("Expected a statement.")
        };
    }
}