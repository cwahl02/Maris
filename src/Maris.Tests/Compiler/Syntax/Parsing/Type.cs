using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Type
{
    private static TypeSyntax ParseTypeFromDeclaration(string typeText)
    {
        var text = $"T :: alias {typeText};";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();
        var aliasDeclaration = Assert.IsType<AliasDeclaration>(statements[0]);
        return aliasDeclaration.Type;
    }

    [Theory]
    [InlineData("u8", SyntaxTokenKind.U8)]
    [InlineData("u16", SyntaxTokenKind.U16)]
    [InlineData("u32", SyntaxTokenKind.U32)]
    [InlineData("u64", SyntaxTokenKind.U64)]
    [InlineData("i8", SyntaxTokenKind.I8)]
    [InlineData("i16", SyntaxTokenKind.I16)]
    [InlineData("i32", SyntaxTokenKind.I32)]
    [InlineData("i64", SyntaxTokenKind.I64)]
    [InlineData("f32", SyntaxTokenKind.F32)]
    [InlineData("f64", SyntaxTokenKind.F64)]
    [InlineData("void", SyntaxTokenKind.Void)]
    [InlineData("bool", SyntaxTokenKind.Bool)]
    [InlineData("string", SyntaxTokenKind.String)]
    public void Parse_BuiltinType(string typeText, SyntaxTokenKind expectedKind)
    {
        var type = Assert.IsType<BuiltinType>(ParseTypeFromDeclaration(typeText));
        Assert.Equal(expectedKind, type.Keyword.Kind);
    }

    [Fact]
    public void Parse_NamedType()
    {
        var type = Assert.IsType<NamedType>(ParseTypeFromDeclaration("Foo"));
        Assert.Single(type.Path.Elements);
    }

    [Fact]
    public void Parse_NamedType_Path()
    {
        var type = Assert.IsType<NamedType>(ParseTypeFromDeclaration("Foo.Bar"));
        Assert.Equal(2, type.Path.Elements.Count());
    }

    [Fact]
    public void Parse_PointerType()
    {
        var type = Assert.IsType<PointerType>(ParseTypeFromDeclaration("*i32"));
        Assert.Equal(SyntaxTokenKind.Star, type.Star.Kind);
        Assert.IsType<BuiltinType>(type.ElementType);
    }

    [Fact]
    public void Parse_ReferenceType()
    {
        var type = Assert.IsType<ReferenceType>(ParseTypeFromDeclaration("&i32"));
        Assert.Equal(SyntaxTokenKind.Ampersand, type.Ampersand.Kind);
        Assert.IsType<BuiltinType>(type.ElementType);
    }

    [Fact]
    public void Parse_SliceType()
    {
        var type = Assert.IsType<SliceType>(ParseTypeFromDeclaration("[]i32"));
        Assert.Equal(SyntaxTokenKind.LeftBracket, type.LeftBracket.Kind);
        Assert.Equal(SyntaxTokenKind.RightBracket, type.RightBracket.Kind);
        Assert.IsType<BuiltinType>(type.ElementType);
    }

    [Fact]
    public void Parse_ArrayType()
    {
        var type = Assert.IsType<ArrayType>(ParseTypeFromDeclaration("[5]i32"));
        Assert.Equal(SyntaxTokenKind.LeftBracket, type.LeftBracket.Kind);
        Assert.Equal(SyntaxTokenKind.IntegerLiteral, type.Size.Kind);
        Assert.Equal(SyntaxTokenKind.RightBracket, type.RightBracket.Kind);
        Assert.IsType<BuiltinType>(type.ElementType);
    }

    [Fact]
    public void Parse_FunctionType()
    {
        var type = Assert.IsType<FunctionType>(ParseTypeFromDeclaration("(i32, i32) -> i32"));
        Assert.Equal(2, type.Parameters.Elements.Count());
        Assert.NotNull(type.Arrow);
        Assert.NotNull(type.ReturnTypes);
        Assert.Single(type.ReturnTypes!.Elements);
    }

    [Fact]
    public void Parse_PointerToPointerType()
    {
        var type = Assert.IsType<PointerType>(ParseTypeFromDeclaration("**i32"));
        var inner = Assert.IsType<PointerType>(type.ElementType);
        Assert.IsType<BuiltinType>(inner.ElementType);
    }
}
