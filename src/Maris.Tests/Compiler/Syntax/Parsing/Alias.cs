using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Alias
{
    private static List<StatementSyntax> Parse(string text)
    {
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        return parser.Parse();
    }

    [Fact]
    public void Parse_AliasDeclaration_BuiltinType()
    {
        var text = "MyInt :: alias i32;";
        var statements = Parse(text);

        var aliasDeclaration = Assert.IsType<AliasDeclaration>(statements[0]);
        Assert.Equal("MyInt", text.Substring(aliasDeclaration.Name.Token.Span.Start, aliasDeclaration.Name.Token.Span.Length));
        Assert.Equal(SyntaxTokenKind.ColonColon, aliasDeclaration.ColonColon.Kind);
        Assert.Equal(SyntaxTokenKind.Alias, aliasDeclaration.AliasKeyword.Kind);
        Assert.Equal(SyntaxTokenKind.Semicolon, aliasDeclaration.Semicolon.Kind);

        var type = Assert.IsType<BuiltinType>(aliasDeclaration.Type);
        Assert.Equal(SyntaxTokenKind.I32, type.Keyword.Kind);
    }

    [Fact]
    public void Parse_AliasDeclaration_NamedType()
    {
        var text = "MyAlias :: alias OtherType;";
        var statements = Parse(text);

        var aliasDeclaration = Assert.IsType<AliasDeclaration>(statements[0]);
        var type = Assert.IsType<NamedType>(aliasDeclaration.Type);
        Assert.Single(type.Path.Elements);
        Assert.Equal("OtherType", text.Substring(type.Path.Elements[0].Token.Span.Start, type.Path.Elements[0].Token.Span.Length));
    }

    [Fact]
    public void Parse_AliasDeclaration_PointerType()
    {
        var text = "IntPointer :: alias *i32;";
        var statements = Parse(text);

        var aliasDeclaration = Assert.IsType<AliasDeclaration>(statements[0]);
        var pointerType = Assert.IsType<PointerType>(aliasDeclaration.Type);
        Assert.Equal(SyntaxTokenKind.Star, pointerType.Star.Kind);
        Assert.IsType<BuiltinType>(pointerType.ElementType);
    }
}
