namespace Compiler.Text;

public sealed class TextWindow
{
    private readonly SourceText _source;
    public TextWindow(SourceText source)
    {
        _source = source;
    }

    /// <summary>
    /// The current position in the source text.
    /// </summary>
    public int Position { get; private set; } = 0;

    /// <summary>
    /// The current line number in the source text.
    /// </summary>
    public int Line { get; private set; } = 1;

    /// <summary>
    /// The current column number in the source text.
    /// </summary>
    public int Column { get; private set; } = 1;

    /// <summary>
    /// Indicates whether the current position is at the end of the source text.
    /// </summary>
    public bool IsAtEnd
        => Position >= _source.Length;

    /// <summary>
    /// Gets the current character in the source text at the current position.
    /// </summary>
    public char Current => _source[Position];

    /// <summary>
    /// Peeks at the character in the source text at the specified offset from the current position.
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public char Peek(int offset) => _source[Position + offset];

    /// <summary>
    /// Gets the next character in the source text after the current position.
    /// </summary>
    public char Next => Peek(1);

    /// <summary>
    /// Gets the previous character in the source text before the current position.
    /// </summary>
    public char Previous => Peek(-1);

    /// <summary>
    /// Advances the current position in the source text by one character, updating the line and column numbers accordingly.
    /// </summary>
    public void Advance()
    {
        if(IsAtEnd) return;

        if(Current == '\n')
        {
            Line++;
            Column = 1;
        }
        else
        {
            Column++;
        }
        Position++;
    }
}