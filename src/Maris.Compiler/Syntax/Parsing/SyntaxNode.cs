namespace Maris.Compiler.Syntax.Parsing;

public abstract record SyntaxNode;
public abstract record StatementSyntax : SyntaxNode;
public abstract record DeclarationSyntax(
    DeclarationAccessibility Accessibility
) : StatementSyntax;

public enum DeclarationAccessibility
{
    Public,
    Private
}

public abstract record ExpressionSyntax : StatementSyntax;