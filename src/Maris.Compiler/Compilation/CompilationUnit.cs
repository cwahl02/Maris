using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Diagnostics;
using Maris.Core.Text;

namespace Maris.Compiler.Compilation;

public enum CompilationPhase
{
    Initial,
    Lexing,
    Parsing,
    Binding
}

public sealed class CompilationUnit
{
    public SourceFile SourceFile { get; }
    public CompilationPhase Phase { get; } = CompilationPhase.Initial;

    public IReadOnlyList<Token>? Tokens { get; }
    public SyntaxNode? SyntaxTree { get; }
    // public BoundNode? BoundTree { get; }

    public DiagnosticBag Diagnostics { get; }
    public bool HasErrors => Diagnostics.HasErrors;
    public CompilationUnit(
        SourceFile sourceFile,
        IReadOnlyList<Token>? tokens,
        SyntaxNode? syntaxTree,
        // BoundNode? boundTree,
        DiagnosticBag diagnostics
        )
    {
        SourceFile = sourceFile;
        Tokens = tokens;
        SyntaxTree = syntaxTree;
        Diagnostics = diagnostics;
    }

    public bool HasTokens => Tokens is not null;
    public bool HasSyntaxTree => SyntaxTree is not null;
    //public bool HasBoundTree => false; // BoundTree is not implemented yet
}