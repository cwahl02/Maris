using Maris.Core.Iterator;
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

    public CompilationUnitSyntax ParseCompilationUnit()
    {
        var items = new List<SyntaxNode>();

        while (!_iterator.IsAtEnd && _iterator.Current.Kind != TokenKind.Eof)
        {
            var start = _iterator.Position;
            items.Add(ParseFileItem());
            
            if (_iterator.Position == start)
            {
                _iterator.Forward();
            }
        }

        var eof = Match(TokenKind.Eof);
        return new CompilationUnitSyntax(items, eof);
    }

    private SyntaxNode ParseFileItem()
    {
        return _iterator.Current.Kind switch
        {
            TokenKind.Module => ParseModuleDeclaration(),
            _ => throw new Exception($"Unexpected token: {_iterator.Current.Kind}"),
        };
    }

    private SyntaxNode ParseModuleDeclaration()
    {
        var moduleKeyword = Match(TokenKind.Module);
        var qualifiedName = ParseQualifiedName();

        if (_iterator.Current.Kind == TokenKind.Semicolon)
        {
            var semicolon = Match(TokenKind.Semicolon);
            return new ModuleDeclarationSyntax(
                moduleKeyword,
                qualifiedName,
                semicolon,
                null,
                null,
                null
            );
        }
        else if (_iterator.Current.Kind == TokenKind.LeftBrace)
        {
            var leftBrace = Match(TokenKind.LeftBrace);
            var bodyItems = new List<SyntaxNode>();

            while (!_iterator.IsAtEnd && _iterator.Current.Kind != TokenKind.RightBrace)
            {
                var start = _iterator.Position;
                bodyItems.Add(ParseFileItem());

                if (_iterator.Position == start)
                {
                    _iterator.Forward();
                }
            }

            var rightBrace = Match(TokenKind.RightBrace);
            return new ModuleDeclarationSyntax(
                moduleKeyword,
                qualifiedName,
                null,
                leftBrace,
                bodyItems,
                rightBrace
            );
        }
        else
        {
            // Handle error: expected ';' or '{'
            throw new Exception($"Expected ';' or '{{' after module declaration, but found {_iterator.Current.Kind}");
        }
    }

    private List<Token> ParseQualifiedName()
    {
        var identifiers = new List<Token>();

        while (!_iterator.IsAtEnd && _iterator.Current.Kind == TokenKind.Identifier)
        {
            identifiers.Add(Match(TokenKind.Identifier));

            if (_iterator.Current.Kind == TokenKind.Dot)
            {
                Match(TokenKind.Dot);
            }
            else
            {
                break;
            }
        }

        return identifiers;
    }
}