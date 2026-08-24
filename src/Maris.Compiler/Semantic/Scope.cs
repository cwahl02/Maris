public sealed class ScopeNode
{
    public ScopeNode? Parent { get; }
    public List<ScopeNode> Children { get; } = [];
    public Dictionary<string, List<Symbol>> Symbols { get; } = [];

    public ScopeNode(ScopeNode? parent = null)
    {
        Parent = parent;
    }
}