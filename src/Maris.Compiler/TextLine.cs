namespace Maris.Compiler;

public readonly struct TextLine
{
    public int Start { get; }
    public int Length { get; }
    public int End => Start + Length;
    public TextLine(int start, int length)
    {
        Start = start;
        Length = length;
    }
}