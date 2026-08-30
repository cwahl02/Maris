public enum SymbolKind
{
    Variable,
    Function,
    Class,
    Module,
    Parameter,
    Property,
    Field,
    Event,
    Constant,
    EnumMember,
    TypeParameter,
    Namespace
}

public sealed class Symbol
{
    public string Name { get; }
    public SymbolKind Kind { get; }

    public Symbol(string name, SymbolKind kind)
    {
        Name = name;
        Kind = kind;
    }
}