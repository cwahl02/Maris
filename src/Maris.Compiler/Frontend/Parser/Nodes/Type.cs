namespace Maris.Compiler.Parser;

public sealed record NamedTypeSyntax(string Name) : TypeNode
{
    public override IEnumerable<SyntaxNode> GetChildren()
        => Enumerable.Empty<SyntaxNode>();
}

public sealed record PointerTypeSyntax(TypeNode BaseType) : TypeNode
{
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return BaseType;
    }
}