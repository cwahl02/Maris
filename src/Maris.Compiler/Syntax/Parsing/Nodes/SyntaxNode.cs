using Maris.Core.Text;
using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public abstract record SyntaxNode(TextSpan Span);
public abstract record ExpressionSyntax(TextSpan Span) : SyntaxNode(Span);
public abstract record StatementSyntax(TextSpan Span) : SyntaxNode(Span);

public abstract record MemberSyntax(TextSpan Span) : SyntaxNode(Span);
public abstract record NameSyntax(TextSpan Span) : SyntaxNode(Span);