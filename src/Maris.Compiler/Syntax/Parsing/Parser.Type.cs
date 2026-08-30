using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public abstract record TypeSyntax : SyntaxNode;

public sealed record BuiltinType(
    SyntaxToken Keyword
) : TypeSyntax;

public sealed record NamedType(
    SeparatedSyntax<TokenSyntax> Path
) : TypeSyntax;

public sealed record PointerType(
    SyntaxToken Star,
    TypeSyntax ElementType
) : TypeSyntax;

public sealed record ReferenceType(
    SyntaxToken Ampersand,
    TypeSyntax ElementType
) : TypeSyntax;

public sealed record SliceType(
    SyntaxToken LeftBracket,
    SyntaxToken RightBracket,
    TypeSyntax ElementType
) : TypeSyntax;

public sealed record ArrayType(
    SyntaxToken LeftBracket,
    SyntaxToken RightBracket,
    TypeSyntax ElementType
) : TypeSyntax;

public sealed record FunctionType(
    SyntaxToken LeftParen,
    SeparatedSyntax<TypeSyntax> Parameters,
    SyntaxToken RightParen,
    SyntaxToken? Arrow,
    SeparatedSyntax<TypeSyntax>? ReturnTypes,
    SyntaxToken LeftBrace,
    SyntaxToken RightBrace
    //BlockSyntax? Body
) : TypeSyntax;


public sealed partial class Parser
{
    private TypeSyntax ParseType()
    {
        return Current.Kind switch
        {
            SyntaxTokenKind.Star => ParsePointerType(),
            SyntaxTokenKind.Ampersand => ParseReferenceType(),
            SyntaxTokenKind.LeftBracket => ParseSliceOrArrayType(),
            SyntaxTokenKind.LeftParen => ParseFunctionType(),

            SyntaxTokenKind.U8 or
            SyntaxTokenKind.U16 or
            SyntaxTokenKind.U32 or
            SyntaxTokenKind.U64 or
            SyntaxTokenKind.I8 or
            SyntaxTokenKind.I16 or
            SyntaxTokenKind.I32 or
            SyntaxTokenKind.I64 or
            SyntaxTokenKind.F32 or
            SyntaxTokenKind.F64 or
            SyntaxTokenKind.Void or
            SyntaxTokenKind.Bool or
            SyntaxTokenKind.String =>
                    ParseBuiltinType(),

            SyntaxTokenKind.Identifier =>
                    ParseNamedType(),

            _ => throw new Exception($"Unexpected token: {Current.Kind}")
        };
    }

    private BuiltinType ParseBuiltinType()
    {
        SyntaxToken keyword = Expect(Current.Kind);
        return new BuiltinType(keyword);
    }

    private NamedType ParseNamedType()
    {
        SeparatedSyntax<TokenSyntax> path = ParseSeparated(() => ParseToken(SyntaxTokenKind.Identifier), SyntaxTokenKind.Dot);
        return new NamedType(path);
    }

    private PointerType ParsePointerType()
    {
        SyntaxToken star = Expect(SyntaxTokenKind.Star);
        TypeSyntax elementType = ParseType();
        return new PointerType(star, elementType);
    }

    private ReferenceType ParseReferenceType()
    {
        SyntaxToken ampersand = Expect(SyntaxTokenKind.Ampersand);
        TypeSyntax elementType = ParseType();
        return new ReferenceType(ampersand, elementType);
    }

    private SliceType ParseSliceType()
    {
        SyntaxToken leftBracket = Expect(SyntaxTokenKind.LeftBracket);
        SyntaxToken rightBracket = Expect(SyntaxTokenKind.RightBracket);
        TypeSyntax elementType = ParseType();
        return new SliceType(leftBracket, rightBracket, elementType);
    }

    private ArrayType ParseArrayType()
    {
        SyntaxToken leftBracket = Expect(SyntaxTokenKind.LeftBracket);
        SyntaxToken rightBracket = Expect(SyntaxTokenKind.RightBracket);
        TypeSyntax elementType = ParseType();
        return new ArrayType(leftBracket, rightBracket, elementType);
    }

    private TypeSyntax ParseSliceOrArrayType()
    {
        SyntaxToken leftBracket = Expect(SyntaxTokenKind.LeftBracket);
        if (Current.Kind == SyntaxTokenKind.RightBracket)
        {
            return ParseSliceType();
        }
        else
        {
            return ParseArrayType();
        }
    }

    private FunctionType ParseFunctionType()
    {
        SyntaxToken leftParen = Expect(SyntaxTokenKind.LeftParen);
        SeparatedSyntax<TypeSyntax> parameters = ParseSeparated(ParseType, SyntaxTokenKind.Comma);
        SyntaxToken rightParen = Expect(SyntaxTokenKind.RightParen);

        SyntaxToken? arrow = Match(SyntaxTokenKind.Arrow) ? Expect(SyntaxTokenKind.Arrow) : null;
        SeparatedSyntax<TypeSyntax>? returnTypes = arrow != null ? ParseSeparated(ParseType, SyntaxTokenKind.Comma) : null;

        SyntaxToken leftBrace = Expect(SyntaxTokenKind.LeftBrace);
        SyntaxToken rightBrace = Expect(SyntaxTokenKind.RightBrace);
        //BlockSyntax? body = Current.Kind == SyntaxTokenKind.LeftBrace ? ParseBlock() : null;

        return new FunctionType(leftParen, parameters, rightParen, arrow, returnTypes, leftBrace, rightBrace);
    }
}