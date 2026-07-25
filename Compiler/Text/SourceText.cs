namespace Compiler.Text;

public sealed class SourceText
{
    private readonly string _text;
    public SourceText(string text)
    {
        _text = text;
    }
    public int Length => _text.Length;
    public char this[int index] => _text[index];
    public override string ToString() => _text;
}