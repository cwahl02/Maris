namespace Maris.Core.Text;

public sealed class TextWindow
{
    private readonly string _text;
    public int Position { get; private set; } = 0;
    public char Current => Position < _text.Length ? _text[Position] : '\0';
    public char Peek(int offset) => Position + offset < _text.Length ? _text[Position + offset] : '\0';
    public bool IsAtEnd => Position >= _text.Length;

    public TextWindow(string text)
    {
        _text = text;
    }

    public void Advance()
    {
        Position++;
    }

    public void Advance(int count)
    {
        Position += count;
    }
}