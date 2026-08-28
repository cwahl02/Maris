using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class If
{
    [Fact]
    public void Parse_IfStatement_Block()
    {
        var text = "if x > 0 {}";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var ifStatement = Assert.IsType<IfStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.If, ifStatement.IfKeyword.Kind);

        var condition = Assert.IsType<BinaryExpression>(ifStatement.Condition);
        var left = Assert.IsType<IdentifierExpression>(condition.Left);
        Assert.Equal("x", text.Substring(left.IdentifierTokens.Elements[0].Token.Span.Start, left.IdentifierTokens.Elements[0].Token.Span.Length));

        Assert.Equal(SyntaxTokenKind.GreaterThan, condition.OperatorToken.Kind);

        var right = Assert.IsType<LiteralExpression>(condition.Right);
        Assert.Equal("0", text.Substring(right.LiteralToken.Span.Start, right.LiteralToken.Span.Length));
    }

    [Fact]
    public void Parse_IfStatement_Single()
    {
        var text = "if x > 0: return 1;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var ifStatement = Assert.IsType<IfStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.If, ifStatement.IfKeyword.Kind);

        var condition = Assert.IsType<BinaryExpression>(ifStatement.Condition);
        var left = Assert.IsType<IdentifierExpression>(condition.Left);
        Assert.Equal("x", text.Substring(left.IdentifierTokens.Elements[0].Token.Span.Start, left.IdentifierTokens.Elements[0].Token.Span.Length));

        Assert.Equal(SyntaxTokenKind.GreaterThan, condition.OperatorToken.Kind);

        var right = Assert.IsType<LiteralExpression>(condition.Right);
        Assert.Equal("0", text.Substring(right.LiteralToken.Span.Start, right.LiteralToken.Span.Length));
    }

    [Fact]
    public void Parse_IfStatement_ElseBlock()
    {
        var text = "if x > 0 {} else {}";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var ifStatement = Assert.IsType<IfStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.If, ifStatement.IfKeyword.Kind);

        var condition = Assert.IsType<BinaryExpression>(ifStatement.Condition);
        var left = Assert.IsType<IdentifierExpression>(condition.Left);
        Assert.Equal("x", text.Substring(left.IdentifierTokens.Elements[0].Token.Span.Start, left.IdentifierTokens.Elements[0].Token.Span.Length));

        Assert.Equal(SyntaxTokenKind.GreaterThan, condition.OperatorToken.Kind);

        var right = Assert.IsType<LiteralExpression>(condition.Right);
        Assert.Equal("0", text.Substring(right.LiteralToken.Span.Start, right.LiteralToken.Span.Length));

        Assert.Equal(SyntaxTokenKind.Else, ifStatement.ElseKeyword!.Kind);
        var elseBlock = Assert.IsType<BlockSyntax>(ifStatement.ElseStatement);
        Assert.NotNull(elseBlock);
    }
}