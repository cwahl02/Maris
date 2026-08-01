namespace Maris.Compiler;

public sealed class TextWindow
{
    private readonly string _text;

    public int Position { get; private set; }

    public int Length => _text.Length;

    public TextWindow(string text)
    {
        _text = text;
    }

    public char Current =>
        Position >= Length ? '\0' : _text[Position];

    public char Peek(int offset = 1)
    {
        int index = Position + offset;
        return index >= Length ? '\0' : _text[index];
    }

    public void Advance()
    {
        Position++;
    }

    public void Advance(int count)
    {
        Position += count;
    }

    public bool EndOfText()
    {
        return Position >= Length;
    }

    public ReadOnlySpan<char> Slice(int start, int length)
    {
        return _text.AsSpan(start, length);
    }
}
