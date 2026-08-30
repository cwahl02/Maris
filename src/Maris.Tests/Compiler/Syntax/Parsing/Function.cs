using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Function
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
    public void Parse_FunctionDeclaration_NoParameters()
    {
        var text = "main :: () { }";
        var statements = Parse(text);

        var function = Assert.IsType<FunctionDeclaration>(statements[0]);
        Assert.Equal("main", text.Substring(function.Name.Token.Span.Start, function.Name.Token.Span.Length));
        Assert.Equal(SyntaxTokenKind.ColonColon, function.ColonColon.Kind);
        Assert.Null(function.Parameters);
        Assert.Null(function.Arrow);
        Assert.Null(function.ReturnTypes);
        Assert.Empty(function.Body.Statements);
    }

    [Fact]
    public void Parse_FunctionDeclaration_WithParametersAndReturnType()
    {
        var text = "add :: (x, y: i32) -> i32 { return x + y; }";
        var statements = Parse(text);

        var function = Assert.IsType<FunctionDeclaration>(statements[0]);
        Assert.NotNull(function.Parameters);
        var group = Assert.Single(function.Parameters!.Elements);
        Assert.Equal(2, group.Names.Elements.Count());
        Assert.Equal(SyntaxTokenKind.Colon, group.Binding.Kind);
        Assert.IsType<BuiltinType>(group.Type);

        Assert.NotNull(function.Arrow);
        Assert.NotNull(function.ReturnTypes);
        var returnType = Assert.IsType<BuiltinType>(function.ReturnTypes!.Elements[0]);
        Assert.Equal(SyntaxTokenKind.I32, returnType.Keyword.Kind);

        Assert.Single(function.Body.Statements);
    }

    [Fact]
    public void Parse_FunctionDeclaration_WithDefaultParameter()
    {
        var text = "myfunc :: (x: i32 = 27) -> i32 { return x * x; }";
        var statements = Parse(text);

        var function = Assert.IsType<FunctionDeclaration>(statements[0]);
        var group = Assert.Single(function.Parameters!.Elements);
        Assert.NotNull(group.EqualToken);
        var defaultValue = Assert.IsType<LiteralExpression>(group.Default);
        Assert.Equal("27", text.Substring(defaultValue.LiteralToken.Span.Start, defaultValue.LiteralToken.Span.Length));
    }

    [Fact]
    public void Parse_FunctionDeclaration_MultipleReturnTypes()
    {
        var text = "divmod :: (x, y: i32) -> i32, i32 { return x, y; }";
        var statements = Parse(text);

        var function = Assert.IsType<FunctionDeclaration>(statements[0]);
        Assert.Equal(2, function.ReturnTypes!.Elements.Count());
    }
}
