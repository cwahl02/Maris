namespace Maris.Compiler;

public sealed class SourceFile
{
    public string Path { get; }
    public string Text { get; }
    public IReadOnlyList<TextLine> Lines { get; }
    public SourceFile(string path, string text)
    {
        Path = path;
        Text = text;
        Lines = BuildLines(text);
    }

    private static IReadOnlyList<TextLine> BuildLines(string text)
    {
        var lines = new List<TextLine>();
        int start = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '\n')
            {
                lines.Add(new TextLine(start, i - start));
                start = i + 1;
            }
        }
        if (start < text.Length)
        {
            lines.Add(new TextLine(start, text.Length - start));
        }
        return lines;
    }
}