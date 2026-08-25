using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed record SwitchSyntax(
    SyntaxToken SwitchKeyword,
    ExpressionSyntax Condition,
    SwitchBodySyntax Body
) : StatementSyntax;

public sealed record SwitchBodySyntax(
    SyntaxToken OpenBrace,
    List<CaseSyntax> Cases,
    DefaultSyntax? DefaultCase,
    SyntaxToken CloseBrace
) : StatementSyntax;

public sealed record DefaultSyntax(
    SyntaxToken DefaultKeyword,
    StatementSyntax Body
) : StatementSyntax;

public sealed record CaseSyntax(
    SyntaxToken CaseKeyword,
    ExpressionSyntax Condition,
    StatementSyntax Body
) : StatementSyntax;

public sealed partial class Parser
{
    private SwitchSyntax ParseSwitch()
    {
        SyntaxToken switchKeyword = Expect(SyntaxTokenKind.Switch);
        ExpressionSyntax condition = ParseExpression();
        SwitchBodySyntax body = ParseSwitchBody();

        return new SwitchSyntax(switchKeyword, condition, body);
    }

    private SwitchBodySyntax ParseSwitchBody()
    {
        SyntaxToken openBrace = Expect(SyntaxTokenKind.LeftBrace);
        List<CaseSyntax> cases = new List<CaseSyntax>();
        DefaultSyntax? defaultCase = null;

        while (_iterator.Current.Kind != SyntaxTokenKind.RightBrace)
        {
            switch (_iterator.Current.Kind)
            {
                case SyntaxTokenKind.Case:
                    cases.Add(ParseCase());
                    break;
                case SyntaxTokenKind.Default:
                    if (defaultCase != null)
                        throw new Exception("Multiple default cases are not allowed.");
                    defaultCase = ParseDefault();
                    break;
                default:
                    throw new Exception($"Unexpected token of kind {_iterator.Current.Kind} at position {_iterator.Position}.");
            }
        }

        SyntaxToken closeBrace = Expect(SyntaxTokenKind.RightBrace);

        return new SwitchBodySyntax(openBrace, cases, defaultCase, closeBrace);
    }

    private CaseSyntax ParseCase()
    {
        SyntaxToken caseKeyword = Expect(SyntaxTokenKind.Case);
        ExpressionSyntax condition = ParseExpression();
        StatementSyntax body = ParseBlock();

        return new CaseSyntax(caseKeyword, condition, body);
    }
}