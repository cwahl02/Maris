using Maris.Core.Iterator;
using Maris.Core.Tree;
using Maris.Compiler.Syntax.Lexing;
using System.Text.RegularExpressions;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private readonly Iterator<Token> _iterator;
    public Parser(IReadOnlyList<Token> tokens)
    {
        _iterator = new Iterator<Token>(tokens);
    }

    public FileSyntax Parse()
    {
        return ParseFile();
    }

    private FileSyntax ParseFile()
    {
        var declarations = new List<DeclarationSyntax>();
        while (!_iterator.IsAtEnd)
        {
            var declaration = ParseFileItem();
            if (declaration is DeclarationSyntax decl)
            {
                declarations.Add(decl);
                continue;
            }

            _iterator.Forward();
        }

        return new FileSyntax(declarations);
    }

    private DeclarationSyntax ParseFileItem()
    {
        
    }

    private bool Expect(TokenKind[] kinds)
    {
        foreach (var kind in kinds)
        {
            if (_iterator.Current.Kind == kind)
            {
                return true;
            }
        }
        return false;
    }

    private bool Expect(TokenKind kind)
    {
        return _iterator.Current.Kind == kind;
    }

    private bool Expect(params TokenKind[][] sequences)
    {
        foreach (var sequence in sequences)
        {
            var match = true;
            foreach (var kind in sequence)
            {
                if (_iterator.Current.Kind != kind)
                {
                    match = false;
                    break;
                }
                _iterator.Forward();
            }

            if (match)
            {
                return true;
            }
        }
        return false;
    }

    private bool MatchSequence(params TokenKind[] sequence)
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            if (_iterator.Peek(i).Kind != sequence[i])
            {
                return false;
            }
        }

        return true;
    }
}

public sealed record FileSyntax(
    List<DeclarationSyntax> Declarations
) : SyntaxNode;