namespace Maris.Compiler;

public sealed class SourceFile
{
    public string FilePath { get; }
    public string FileName => System.IO.Path.GetFileName(FilePath);
    public string Text { get; }
    
    public SourceFile(string filePath)
    {
        FilePath = filePath;
        Text = System.IO.File.ReadAllText(filePath);
    }
}