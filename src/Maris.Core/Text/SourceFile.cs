namespace Maris.Core.Text;

public sealed class SourceFile
{
    public string FilePath { get; }
    public string Text { get; }

    public SourceFile(string filePath, string text)
    {
        FilePath = filePath;
        Text = text;
    }
}