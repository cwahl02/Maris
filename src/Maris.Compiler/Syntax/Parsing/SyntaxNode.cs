using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public abstract record SyntaxNode(TextSpan Span);

public sealed record CompilationUnitSyntax(
    IReadOnlyList<SyntaxNode> Items,
    Token Eof
) : SyntaxNode(GetSpan(Items, Eof))
{
    private static TextSpan GetSpan(IReadOnlyList<SyntaxNode> items, Token eof)
    {
        if (items.Count == 0)
            return eof.Span;

        var first = items[0].Span;
        var last = eof.Span;
        return TextSpan.FromBounds(first.Start, last.End);
    }
}

public sealed record ModuleDeclarationSyntax(
    Token ModuleKeyword,
    IReadOnlyList<Token> QualifiedName,
    Token? Semicolon,
    Token? LeftBrace,
    IReadOnlyList<SyntaxNode>? BodyItems,
    Token? RightBrace
) : SyntaxNode(GetSpan(ModuleKeyword, QualifiedName, Semicolon, LeftBrace, BodyItems, RightBrace))
{
    private static TextSpan GetSpan(
        Token ModuleKeyword,
        IReadOnlyList<Token> QualifiedName,
        Token? Semicolon,
        Token? LeftBrace,
        IReadOnlyList<SyntaxNode>? BodyItems,
        Token? RightBrace
    )
    {
        var fallbackEnd = QualifiedName.Count > 0 ? QualifiedName[^1].Span.End : ModuleKeyword.Span.End;
        var end = Semicolon?.Span.End ?? LeftBrace?.Span.End ?? RightBrace?.Span.End ?? fallbackEnd;
        return TextSpan.FromBounds(ModuleKeyword.Span.Start, end);
    }
}