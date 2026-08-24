namespace Maris.Core.Tree;

public sealed class Tree<T>
{
    public TreeNode<T>? Root { get; private set; }

    public TreeNode<T> SetRoot(T value)
    {
        Root = new TreeNode<T>(value);
        return Root;
    }

    public IEnumerable<TreeNode<T>> TraversePreOrder(TreeNode<T>? node)
    {
        if (node == null)
            yield break;

        yield return node;

        foreach (var child in node.Children)
        {
            foreach (var descendant in TraversePreOrder(child))
            {
                yield return descendant;
            }
        }
    }

    public IEnumerable<TreeNode<T>> TraversePostOrder(TreeNode<T>? node)
    {
        if (node == null)
            yield break;

        foreach (var child in node.Children)
        {
            foreach (var descendant in TraversePostOrder(child))
            {
                yield return descendant;
            }
        }

        yield return node;
    }

    public IEnumerable<TreeNode<T>> TraverseBreadthFirst(TreeNode<T>? node)
    {
        if (node == null)
            yield break;

        var queue = new Queue<TreeNode<T>>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            yield return current;

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }
        }
    }

    public IEnumerable<TreeNode<T>> TraverseDepthFirst(TreeNode<T>? node)
    {
        if (node == null)
            yield break;

        var stack = new Stack<TreeNode<T>>();
        stack.Push(node);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            yield return current;

            for (int i = current.Children.Count - 1; i >= 0; i--)
            {
                stack.Push(current.Children[i]);
            }
        }
    }

    public IEnumerable<TreeNode<T>> Traverse(TreeNode<T>? node, Func<TreeNode<T>, IEnumerable<TreeNode<T>>> traversalMethod)
    {
        if (node == null)
            yield break;

        foreach (var n in traversalMethod(node))
        {
            yield return n;
        }
    }

    // public IEnumerable<TreeNode<T>> TraversePreOrder() => Traverse(Root, TraversePreOrder);
    // public IEnumerable<TreeNode<T>> TraversePostOrder() => Traverse(Root, TraversePostOrder);
    // public IEnumerable<TreeNode<T>> TraverseBreadthFirst() => Traverse(Root, TraverseBreadthFirst);
    // public IEnumerable<TreeNode<T>> TraverseDepthFirst() => Traverse(Root, TraverseDepthFirst);
}

public sealed class TreeNode<T>
{
    public T Value { get; }
    public TreeNode<T>? Parent { get; private set; }
    public List<TreeNode<T>> Children { get; } = new List<TreeNode<T>>();

    public TreeNode(T value)
    {
        Value = value;
    }

    public TreeNode<T> AddChild(T value)
    {
        var childNode = new TreeNode<T>(value) { Parent = this };
        Children.Add(childNode);
        return childNode;
    }
}