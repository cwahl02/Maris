namespace Maris.Core.Iterator;

public sealed class Iterator<T>
{
    private readonly IReadOnlyList<T> _items;
    public int Position = 0;

    public Iterator(IReadOnlyList<T> items)
    {
        _items = items;
    }

    public int Count => _items.Count;
    public bool HasNext => Position < _items.Count - 1;
    public bool HasPrevious => Position > 0;
    public bool IsAtEnd => Position >= _items.Count;
    public T Current => Position < _items.Count ? _items[Position] : default!;
    public T Peek(int offset = 0) => Position + offset < _items.Count ? _items[Position + offset] : default!;

    public void Forward()
    {
        if (HasNext)
            Position++;
    }

    public void Forward(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (HasNext)
                Position++;
            else
                break;
        }
    }

    public void Backward()
    {
        if (HasPrevious)
            Position--;
    }

    public void Backward(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (HasPrevious)
                Position--;
            else
                break;
        }
    }
}