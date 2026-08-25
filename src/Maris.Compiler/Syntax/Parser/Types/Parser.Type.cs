using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public abstract record TypeSyntax : SyntaxNode;

public sealed record PointerTypeSyntax(
    SyntaxToken Star,
    TypeSyntax next
) : TypeSyntax;

public sealed record ReferenceTypeSyntax(
    SyntaxToken Ampersand,
    TypeSyntax next
) : TypeSyntax;

public sealed record ArrayTypeSyntax(
    SyntaxToken LeftBracket,
    ExpressionSyntax Size,
    SyntaxToken CloseBracket,
    TypeSyntax next
) : TypeSyntax;

public sealed record SliceTypeSyntax(
    SyntaxToken LeftBracket,
    SyntaxToken CloseBracket,
    TypeSyntax next
) : TypeSyntax;

public sealed record FunctionTypeSyntax(
    SyntaxToken OpenParen,
    ParameterListSyntax Parameters,
    SyntaxToken CloseParen,
    SyntaxToken Arrow,
    TypeSyntax ReturnType
) : TypeSyntax;

public sealed record GenericTypeSyntax(
    IdentifierPathSyntax Name,
    SyntaxToken LessThan,
    TypeListSyntax TypeArguments,
    SyntaxToken GreaterThan,
    TypeSyntax? Next
) : TypeSyntax;

public sealed record BuiltinTypeSyntax(
    SyntaxToken Keyword,
    TypeSyntax? Next
) : TypeSyntax;

public sealed record NamedTypeSyntax(
    IdentifierPathSyntax Name,
    TypeSyntax? Next
) : TypeSyntax;

public sealed partial class Parser
{
    private TypeSyntax ParseType()
    {
        switch (_iterator.Current.Kind)
        {
            case SyntaxTokenKind.Star:
                SyntaxToken star = Expect(SyntaxTokenKind.Star);
                TypeSyntax next = ParseType();
                return new PointerTypeSyntax(star, next);

            case SyntaxTokenKind.Ampersand:
                SyntaxToken ampersand = Expect(SyntaxTokenKind.Ampersand);
                TypeSyntax nextRef = ParseType();
                return new ReferenceTypeSyntax(ampersand, nextRef);

            case SyntaxTokenKind.LeftBracket:
                SyntaxToken leftBracket = Expect(SyntaxTokenKind.LeftBracket);
                ExpressionSyntax size = ParseExpression();
                SyntaxToken rightBracket = Expect(SyntaxTokenKind.RightBracket);
                TypeSyntax nextArray = ParseType();
                return new ArrayTypeSyntax(leftBracket, size, rightBracket, nextArray);

            case SyntaxTokenKind.LeftParen:
                SyntaxToken leftParen = Expect(SyntaxTokenKind.LeftParen);
                ParameterListSyntax parameters = ParseParameterList();
                SyntaxToken rightParen = Expect(SyntaxTokenKind.RightParen);
                SyntaxToken arrow = Expect(SyntaxTokenKind.Arrow);
                TypeSyntax returnType = ParseType();
                return new FunctionTypeSyntax(leftParen, parameters, rightParen, arrow, returnType);
        }

        
    }
}