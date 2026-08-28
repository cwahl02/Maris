using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Return
{
    [Fact]
    public void Parse_ReturnDeclaration()
    {
        var text = "return 42;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var returnStatement = Assert.IsType<ReturnStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Return, returnStatement.ReturnKeyword.Kind);

        var expression = Assert.IsType<SeparatedSyntax<ExpressionSyntax>>(returnStatement.Expressions);
        var literalExpression = Assert.IsType<LiteralExpression>(expression.Elements[0]);
        Assert.Equal("42", text.Substring(literalExpression.LiteralToken.Span.Start, literalExpression.LiteralToken.Span.Length));
    }

    [Fact]
    public void Parse_ReturnDeclarationWithoutExpression()
    {
        var text = "return;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);

        var returnStatement = Assert.IsType<ReturnStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Return, returnStatement.ReturnKeyword.Kind);
        Assert.Null(returnStatement.Expressions);
    }

    [Fact]
    public void Parse_ReturnDeclarationWithMultipleExpressions()
    {
        var text = "return 1, 2, 3;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);

        var returnStatement = Assert.IsType<ReturnStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Return, returnStatement.ReturnKeyword.Kind);

        var expression = Assert.IsType<SeparatedSyntax<ExpressionSyntax>>(returnStatement.Expressions);
        var literalExpression1 = Assert.IsType<LiteralExpression>(expression.Elements[0]);
        Assert.Equal("1", text.Substring(literalExpression1.LiteralToken.Span.Start, literalExpression1.LiteralToken.Span.Length));
        var literalExpression2 = Assert.IsType<LiteralExpression>(expression.Elements[1]);
        Assert.Equal("2", text.Substring(literalExpression2.LiteralToken.Span.Start, literalExpression2.LiteralToken.Span.Length));
        var literalExpression3 = Assert.IsType<LiteralExpression>(expression.Elements[2]);
        Assert.Equal("3", text.Substring(literalExpression3.LiteralToken.Span.Start, literalExpression3.LiteralToken.Span.Length));
    }
}