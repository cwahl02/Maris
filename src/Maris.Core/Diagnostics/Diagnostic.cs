namespace Maris.Core.Diagnostics;

using Maris.Core.Text;

public sealed record Diagnostic(
    DiagnosticSeverity Severity,
    string Message,
    TextSpan Span
);

public enum DiagnosticSeverity
{
    Info,
    Warning,
    Error
}

public sealed class DiagnosticBag
{
    private readonly List<Diagnostic> _diagnostics = new();

    public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics;

    public void Add(Diagnostic diagnostic)
    {
        _diagnostics.Add(diagnostic);
    }

    public void AddRange(IEnumerable<Diagnostic> diagnostics)
    {
        _diagnostics.AddRange(diagnostics);
    }
}