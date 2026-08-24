using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private StatementSyntax ParseControlFlowBody()
    {
        if (_iterator.Current.Kind == SyntaxTokenKind.LeftBrace)
        {
            return ParseBlock();
        }
        else if (_iterator.Current.Kind == SyntaxTokenKind.Colon)
        {
            Expect(SyntaxTokenKind.Colon);
            return ParseStatement();
        }
        else
        {
            throw new Exception($"Unexpected token of kind {_iterator.Current.Kind} at position {_iterator.Position}.");
        }
    }
}