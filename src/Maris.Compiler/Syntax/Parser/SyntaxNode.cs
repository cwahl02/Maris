namespace Maris.Compiler.Syntax.Parsing;

public abstract record SyntaxNode;
public abstract record StatementSyntax : SyntaxNode;
public abstract record DeclarationSyntax : StatementSyntax;
public abstract record ExpressionSyntax : StatementSyntax;