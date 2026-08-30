using Maris.Compiler.Semantic.Binding;
using Maris.Core.Diagnostics;

namespace Maris.Compiler.Compilation;

public sealed class SemanticArtifacts
{
    public BoundNode? BoundTree { get; }
    public DiagnosticBag Diagnostics { get; } = new();

    public SemanticArtifacts(BoundNode? boundTree, DiagnosticBag diagnostics)
    {
        BoundTree = boundTree;
        Diagnostics = diagnostics;
    }
}