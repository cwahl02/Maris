using Maris.Core.Text;

namespace Maris.Compiler.Compilation;

public enum CompilationPhase
{
    Initial,
    Lexing,
    Parsing,
    Binding
}

public sealed class Compilation
{
    public CompilationPhase Phase { get; } = CompilationPhase.Initial;
    public ScopeNode GlobalScope { get; } = new ScopeNode();
    public List<SourceArtifacts> Units { get; } = [];
    public SemanticArtifacts? SemanticArtifacts { get; private set; }

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