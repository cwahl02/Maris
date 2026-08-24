using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseIdentifierPath()
    {
        var identifier = Match(Lexing.TokenKind.Identifier);
        IdentifierPathSyntax identifierPath = new IdentifierPathSyntax(null, null, identifier);

        while(_iterator.Current.Kind == Lexing.TokenKind.Dot)
        {
            var dot = Match(Lexing.TokenKind.Dot);
            identifier = Match(Lexing.TokenKind.Identifier);
            identifierPath = new IdentifierPathSyntax(identifierPath, dot, identifier);
        }

        return identifierPath;
    }
}

public sealed record IdentifierPathSyntax(IdentifierPathSyntax? Left, Token? Dot, Token Identifier) : SyntaxNode;