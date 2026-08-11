namespace Maris.Compiler.Parser;

public abstract record SyntaxNode
{
    public abstract IEnumerable<SyntaxNode> GetChildren();
}

public abstract record StatementNode : SyntaxNode;
public abstract record ExpressionNode : SyntaxNode;
public abstract record TypeNode : SyntaxNode;