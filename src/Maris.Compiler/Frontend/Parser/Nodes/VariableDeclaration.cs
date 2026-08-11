namespace Maris.Compiler.Parser;

public sealed record VariableDeclaration(
    string Name,
    TypeNode Type,
    ExpressionNode Initializer
    ) : SyntaxNode
{
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Type;
        if (Initializer != null) yield return Initializer;
    }
}