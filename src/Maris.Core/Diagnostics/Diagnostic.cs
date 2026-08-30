namespace Maris.Core.Diagnostics;

using Maris.Core.Text;

public enum DiagnosticSeverity
{
    Info,
    Warning,
    Error
}

public sealed class Diagnostic
{
    public DiagnosticSeverity Severity { get; }
    public string Code { get; }
    public string Message { get; }
    public TextSpan Span { get; }

    public Diagnostic(DiagnosticSeverity severity, string code, string message, TextSpan span)
    {
        Severity = severity;
        Code = code;
        Message = message;
        Span = span;
    }
}