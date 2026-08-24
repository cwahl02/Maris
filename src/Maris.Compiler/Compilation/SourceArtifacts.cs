using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Diagnostics;
using Maris.Core.Text;

namespace Maris.Compiler.Compilation;

public sealed class SourceArtifacts
{
    public SourceFile SourceFile { get; }

    public IReadOnlyList<SyntaxToken>? Tokens { get; }
    public SyntaxNode? SyntaxTree { get; }
    // public BoundNode? BoundTree { get; }

    public DiagnosticBag Diagnostics { get; }
    public bool HasErrors => Diagnostics.HasErrors;
    public SourceArtifacts(
        SourceFile sourceFile,
        IReadOnlyList<SyntaxToken>? tokens,
        SyntaxNode? syntaxTree,
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