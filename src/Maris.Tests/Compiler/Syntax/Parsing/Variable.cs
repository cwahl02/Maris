using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Variable
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
    public void Parse_VariableDeclaration_WithTypeAndInitializer()
    {
        var text = "x : i32 = 5;";
        var statements = Parse(text);

        var variableDeclaration = Assert.IsType<VariableDeclaration>(statements[0]);
        Assert.Single(variableDeclaration.Names.Elements);
        Assert.Equal(SyntaxTokenKind.Colon, variableDeclaration.Binding.Kind);

        var type = Assert.IsType<BuiltinType>(variableDeclaration.Type);
        Assert.Equal(SyntaxTokenKind.I32, type.Keyword.Kind);

        Assert.NotNull(variableDeclaration.EqualToken);
        var initializer = Assert.IsType<LiteralExpression>(variableDeclaration.Initializer);
        Assert.Equal("5", text.Substring(initializer.LiteralToken.Span.Start, initializer.LiteralToken.Span.Length));
    }

    [Fact]
    public void Parse_VariableDeclaration_TypeOnly()
    {
        var text = "x : i32;";
        var statements = Parse(text);

        var variableDeclaration = Assert.IsType<VariableDeclaration>(statements[0]);
        Assert.NotNull(variableDeclaration.Type);
        Assert.Null(variableDeclaration.EqualToken);
        Assert.Null(variableDeclaration.Initializer);
    }

    [Fact]
    public void Parse_VariableDeclaration_InferredType()
    {
        var text = "x := 5;";
        var statements = Parse(text);

        var variableDeclaration = Assert.IsType<VariableDeclaration>(statements[0]);
        Assert.Equal(SyntaxTokenKind.ColonEqual, variableDeclaration.Binding.Kind);
        Assert.Null(variableDeclaration.Type);
        Assert.NotNull(variableDeclaration.Initializer);
    }

    [Fact]
    public void Parse_VariableDeclaration_Constant()
    {
        var text = "x :: i32 = 5;";
        var statements = Parse(text);

        var variableDeclaration = Assert.IsType<VariableDeclaration>(statements[0]);
        Assert.Equal(SyntaxTokenKind.ColonColon, variableDeclaration.Binding.Kind);
        Assert.NotNull(variableDeclaration.Type);
        Assert.NotNull(variableDeclaration.Initializer);
    }

    [Fact]
    public void Parse_VariableDeclaration_MultipleNames()
    {
        var text = "x, y : i32;";
        var statements = Parse(text);

        var variableDeclaration = Assert.IsType<VariableDeclaration>(statements[0]);
        Assert.Equal(2, variableDeclaration.Names.Elements.Count());
    }
}
