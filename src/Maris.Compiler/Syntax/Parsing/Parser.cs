using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private readonly IReadOnlyList<SyntaxToken> _tokens;
    private int _position;

    public Parser(IReadOnlyList<SyntaxToken> tokens)
    {
        _tokens = tokens;
    }

    public List<StatementSyntax> Parse()
    {
        List<StatementSyntax> statements = new();
        while (!IsAtEnd)
        {
            statements.Add(ParseStatement());
        }
        return statements;
    }

    // ==================== Core Combinators ====================

    private bool IsAtEnd => Current.Kind == SyntaxTokenKind.Eof;

    private SyntaxToken Current => Peek(0);

    private SyntaxToken Previous => Peek(-1);

    private SyntaxToken Peek(int offset)
    {
        int index = _position + offset;
        return index >= 0 && index < _tokens.Count ? _tokens[index] : SyntaxToken.Eof;
    }

    private bool Check(SyntaxTokenKind kind) => Current.Kind == kind;

    private bool Check(params SyntaxTokenKind[] kinds) => kinds.Contains(Current.Kind);

    private SyntaxToken Advance()
    {
        SyntaxToken token = Current;
        if (!IsAtEnd)
        {
            _position++;
        }
        return token;
    }

    private bool Match(SyntaxTokenKind kind)
    {
        if (!Check(kind))
        {
            return false;
        }

        Advance();
        return true;
    }

    private bool Match(params SyntaxTokenKind[] kinds)
    {
        if (!Check(kinds))
        {
            return false;
        }

        Advance();
        return true;
    }

    private SyntaxToken Expect(SyntaxTokenKind kind)
    {
        if (!Check(kind))
        {
            throw new ParseException($"Expected token of kind {kind}, but got {Current.Kind} at position {Current.Span.Start}");
        }

        return Advance();
    }

    private SyntaxToken Expect(params SyntaxTokenKind[] kinds)
    {
        if (!Check(kinds))
        {
            throw new ParseException($"Expected token of kind {string.Join(", ", kinds)}, but got {Current.Kind} at position {Current.Span.Start}");
        }

        return Advance();
    }
}

public sealed class ParseException : Exception
{
    public ParseException(string message) : base(message) { }
}
