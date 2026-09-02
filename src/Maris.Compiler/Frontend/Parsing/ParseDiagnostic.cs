namespace Maris.Compiler.Parsing;

/// <summary>
/// Represents a diagnostic message produced while parsing (e.g. a syntax error).
/// Parsing never throws for malformed input; instead diagnostics are collected so
/// callers can inspect them without the parser crashing on unexpected token streams.
/// </summary>
public sealed class ParseDiagnostic(string message, int position)
{
    public string Message { get; } = message;
    public int Position { get; } = position;

    public override string ToString() => $"{Message} (at {Position})";
}
