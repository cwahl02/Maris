using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private StatementSyntax ParseStatement()
    {
        SyntaxTokenKind kind = _iterator.Current.Kind;
        switch (kind)
        {
            case SyntaxTokenKind.If:
            case SyntaxTokenKind.While:
            case SyntaxTokenKind.For:
            case SyntaxTokenKind.Switch:
            case SyntaxTokenKind.Break:
            case SyntaxTokenKind.Continue:
            case SyntaxTokenKind.Return:
            case SyntaxTokenKind.Defer:
                return ParseControl();

            case SyntaxTokenKind.LeftBrace:
                return ParseBlock();

            case SyntaxTokenKind.Identifier:
                SyntaxTokenKind binding = _iterator.Peek(1).Kind;
                if (binding == SyntaxTokenKind.Colon ||
                    binding == SyntaxTokenKind.ColonEqual ||
                    binding == SyntaxTokenKind.ColonColon ||
                    binding == SyntaxTokenKind.ColonColonEqual)
                {
                    return ParseDeclaration();
                }
                else
                {
                    return ParseExpressionStatement();
                }

            default:
                if (kind == SyntaxTokenKind.Identifier ||
                    kind == SyntaxTokenKind.CharacterLiteral ||
                    kind == SyntaxTokenKind.StringLiteral ||
                    kind == SyntaxTokenKind.IntegerLiteral ||
                    kind == SyntaxTokenKind.FloatLiteral ||
                    kind == SyntaxTokenKind.True ||
                    kind == SyntaxTokenKind.False ||
                    kind == SyntaxTokenKind.LeftParen ||
                    kind == SyntaxTokenKind.Plus ||
                    kind == SyntaxTokenKind.Minus ||
                    kind == SyntaxTokenKind.Star)
                {
                    return ParseExpressionStatement();
                }

                throw new Exception($"Unexpected token of kind {kind} at position {_iterator.Position}.");
        }
    }
}