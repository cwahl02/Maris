namespace Maris.Core.Text;

public sealed class SourceFile
{
    public string FilePath { get; }
    public string Name { get; }
    public string Text { get; }
    public int Length => Text.Length;

    public SourceFile(string filePath, string text)
    {
        FilePath = filePath;
        Name = System.IO.Path.GetFileName(filePath);
        Text = text;
    }
}