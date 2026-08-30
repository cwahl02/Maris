namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseExpression()
    {
        return ParseAssignmentExpression();
    }
}