namespace Maris.Compiler.Parsing;

using Maris.Compiler.Lexing;

public abstract class SyntaxNode
{
}

public sealed class ProgramNode : SyntaxNode
{
    public List<SyntaxNode> Declarations { get; } = [];
}

public sealed class ImportDeclarationNode(List<Token> pathParts) : SyntaxNode
{
    public List<Token> PathParts { get; } = pathParts;
}

public sealed class FunctionDeclarationNode(Token name, Token returnType, BlockNode body) : SyntaxNode
{
    public Token Name { get; } = name;
    public Token ReturnType { get; } = returnType;
    public BlockNode Body { get; } = body;
}

public sealed class BlockNode : SyntaxNode
{
    public List<SyntaxNode> Statements { get; } = [];
}

public sealed class ReturnStatementNode(SyntaxNode? expression) : SyntaxNode
{
    public SyntaxNode? Expression { get; } = expression;
}

public sealed class ExpressionStatementNode(SyntaxNode expression) : SyntaxNode
{
    public SyntaxNode Expression { get; } = expression;
}

public sealed class CallExpressionNode(SyntaxNode callee, List<SyntaxNode> arguments) : SyntaxNode
{
    public SyntaxNode Callee { get; } = callee;
    public List<SyntaxNode> Arguments { get; } = arguments;
}

public sealed class IdentifierExpressionNode(Token name) : SyntaxNode
{
    public Token Name { get; } = name;
}

public sealed class LiteralExpressionNode(Token literal) : SyntaxNode
{
    public Token Literal { get; } = literal;
}

/// <summary>
/// Placeholder node produced when the parser encounters malformed input it could not
/// otherwise represent. Recorded alongside a matching <see cref="ParseDiagnostic"/>.
/// </summary>
public sealed class ErrorNode(int position) : SyntaxNode
{
    public int Position { get; } = position;
}
