namespace Maris.Core.Diagnostics;

public sealed class DiagnosticBag
{
    private readonly List<Diagnostic> _items = [];

    public IReadOnlyList<Diagnostic> Items => _items;
    public bool HasErrors => _items.Any(d => d.Severity == DiagnosticSeverity.Error);
    public void Add(Diagnostic diagnostic) => _items.Add(diagnostic);
    public void AddRange(IEnumerable<Diagnostic> diagnostics) => _items.AddRange(diagnostics);
}