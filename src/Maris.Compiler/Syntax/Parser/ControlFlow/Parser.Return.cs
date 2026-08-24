using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record ReturnSyntax(
    SyntaxToken ReturnKeyword,
    ExpressionListSyntax? Expressions,
    SyntaxToken Semicolon
) : StatementSyntax;

public sealed partial class Parser
{
    private ReturnSyntax ParseReturn()
    {
        SyntaxToken returnKeyword = Expect(SyntaxTokenKind.Return);
        ExpressionListSyntax? expressions = null;

        if (_iterator.Current.Kind != SyntaxTokenKind.Semicolon)
        {
            expressions = ParseExpressionList();
        }

        SyntaxToken semicolon = Expect(SyntaxTokenKind.Semicolon);

        return new ReturnSyntax(returnKeyword, expressions, semicolon);
    }
}