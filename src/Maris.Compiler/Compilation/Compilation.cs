namespace Maris.Compiler.Compilation;

public sealed class Compilation
{
    public ScopeNode GlobalScope { get; } = new ScopeNode();
    public List<CompilationUnit> Units { get; } = [];

    public Compilation(IEnumerable<string> FilePaths)
    {
        foreach (var filePath in FilePaths)
        {
            var source = new SourceFile(filePath);
            //var unit = new CompilationUnit(source);
            //Units.Add(unit);
        }
    }
}