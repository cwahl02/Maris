using Maris.Compiler.Lexing;
using Maris.Compiler.Parsing.Syntax;

namespace Maris.Compiler;

public sealed class CompilationUnit
{
    public SourceFile SourceFile { get; }
    public List<Token>? Tokens { get; }
    public SyntaxNode? SyntaxTree { get; }
    public CompilationUnit(SourceFile sourceFile)
    {
        SourceFile = sourceFile;
    }
}