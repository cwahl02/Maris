using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Iterator;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private Iterator<SyntaxToken> _iterator;

    public Parser(List<SyntaxToken> tokens)
    {
        _iterator = new Iterator<SyntaxToken>(tokens);
    }

    public List<StatementSyntax> Parse()
    {
        List<StatementSyntax> statements = new List<StatementSyntax>();
        while (Current.Kind != SyntaxTokenKind.Eof)
        {
            statements.Add(ParseStatement());
        }
        return statements;
    }

    private SyntaxToken Current => _iterator.Current;
    private SyntaxToken Peek(int offset = 0) => _iterator.Peek(offset);
    private void Forward() => _iterator.Forward();
    private void Backward() => _iterator.Backward();
    private int Position => _iterator.Position;

    private SyntaxToken Expect(SyntaxTokenKind kind)
    {
        if (Current.Kind != kind)
        {
            throw new Exception($"Expected token of kind {kind}, but got {Current.Kind} at position {Current.Span.Start}");
        }

        var token = Current;
        Forward();
        return token;
    }

    private SyntaxToken Expect(params SyntaxTokenKind[] kinds)
    {
        if (!kinds.Contains(Current.Kind))
        {
            throw new Exception($"Expected token of kind {string.Join(", ", kinds)}, but got {Current.Kind} at position {Current.Span.Start}");
        }

        var token = Current;
        Forward();
        return token;
    }

    private bool Match(SyntaxTokenKind kind)
    {
        return Current.Kind == kind;
    }

    private bool Match(params SyntaxTokenKind[] kinds)
    {
        return kinds.Contains(Current.Kind);
    }
}