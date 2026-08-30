namespace Maris.Core.Text;

public readonly struct TextSpan
{
    public int Start { get; }
    public int Length { get; }
    public int End => Start + Length;
    public static readonly TextSpan Empty = new(0, 0);
    public static TextSpan FromBounds(int start, int end) => new(start, end - start);

    public TextSpan(int start, int length)
    {
        Start = start;
        Length = length;
    }

    public bool Contains(int position) => position >= Start && position < End;
}
