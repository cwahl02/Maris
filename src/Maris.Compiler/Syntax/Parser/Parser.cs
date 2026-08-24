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

    public List<DeclarationSyntax> Parse()
    {
        List<DeclarationSyntax> declarations = new();

        while (_iterator.Current.Kind != SyntaxTokenKind.Eof)
        {
            declarations.Add(ParseDeclaration());
        }

        return declarations;
    }

    private SyntaxToken Expect(SyntaxTokenKind expected)
    {
        if (_iterator.Current.Kind != expected)
        {
            throw new Exception($"Expected token of kind {expected}, but got {_iterator.Current.Kind}.");
        }

        SyntaxToken token = _iterator.Current;
        _iterator.Forward();
        return token;
    }
}