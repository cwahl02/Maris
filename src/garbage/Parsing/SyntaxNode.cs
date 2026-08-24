namespace Maris.Compiler.Syntax.Parsing;

public abstract record SyntaxNode;
public abstract record StatementSyntax : SyntaxNode;
public abstract record ExpressionSyntax : SyntaxNode;
public abstract record DeclarationSyntax : SyntaxNode;

public sealed record ExpressionListSyntax(IReadOnlyList<ExpressionSyntax> Items) : ExpressionSyntax;
public sealed record IdentifierListSyntax(IReadOnlyList<Lexing.Token> Identifiers) : ExpressionSyntax;
public sealed record IdentifierPathSyntax(IReadOnlyList<Lexing.Token> Parts) : ExpressionSyntax;

public sealed record BlockSyntax(IReadOnlyList<StatementSyntax> Statements) : StatementSyntax;

public sealed record ParameterListSyntax(IReadOnlyList<ParameterGroupSyntax> Groups) : SyntaxNode;
public sealed record ParameterGroupSyntax(IdentifierListSyntax Identifiers, TypeSyntax Type) : SyntaxNode;

public abstract record TypeSyntax : SyntaxNode;
public sealed record TypeListSyntax(IReadOnlyList<TypeSyntax> Types) : SyntaxNode;

public sealed record IfSyntax(
    Lexing.Token IfKeyword,
    ExpressionSyntax Condition,
    SyntaxNode ThenStatement,
    SyntaxNode? ElseStatement
) : StatementSyntax;

public sealed record WhileSyntax(
    Lexing.Token WhileKeyword,
    ExpressionSyntax Condition,
    SyntaxNode Body
) : StatementSyntax;

public sealed record ContinueSyntax(Lexing.Token ContinueKeyword) : StatementSyntax;
public sealed record BreakSyntax(Lexing.Token BreakKeyword) : StatementSyntax;
public sealed record ReturnSyntax(
    Lexing.Token ReturnKeyword,
    ExpressionListSyntax? ReturnValues
) : StatementSyntax;

public sealed record VariableDeclarationSyntax(
    Lexing.Token Identifier,
    Lexing.TokenKind BindingOperator,
    TypeSyntax Type,
    ExpressionSyntax? Initializer
) : DeclarationSyntax;

public sealed record IdentifierSyntax(Lexing.Token Identifier) : ExpressionSyntax;
public sealed record LiteralSyntax(Lexing.Token Literal) : ExpressionSyntax;
public sealed record UnaryExpressionSyntax(
    Lexing.Token Operator,
    ExpressionSyntax Operand
) : ExpressionSyntax;

public sealed record BinaryExpressionSyntax(
    ExpressionSyntax Left,
    Lexing.Token Operator,
    ExpressionSyntax Right
) : ExpressionSyntax;

public sealed record AssignmentExpressionSyntax(
    ExpressionSyntax Target,
    Lexing.Token Operator,
    ExpressionSyntax Value
) : ExpressionSyntax;

public sealed record CallExpressionSyntax(
    ExpressionSyntax Callee,
    ExpressionListSyntax Arguments
) : ExpressionSyntax;

public sealed record NamedTypeSyntax(IdentifierPathSyntax TypeName) : TypeSyntax;
public sealed record PointerTypeSyntax(TypeSyntax Inner) : TypeSyntax;
public sealed record ArrayTypeSyntax(TypeSyntax Inner) : TypeSyntax;
public sealed record SliceTypeSyntax(TypeSyntax Inner) : TypeSyntax;