using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class For
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
    public void Parse_ForStatement_AllClauses()
    {
        var statements = Parse("for i = 0; i < 10; i = i + 1 {}");

        var forStatement = Assert.IsType<ForStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.For, forStatement.ForKeyword.Kind);

        Assert.NotNull(forStatement.Initializer);
        Assert.IsType<AssignmentExpression>(forStatement.Initializer);

        Assert.NotNull(forStatement.Condition);
        Assert.IsType<BinaryExpression>(forStatement.Condition);

        Assert.NotNull(forStatement.Iteration);
        Assert.IsType<AssignmentExpression>(forStatement.Iteration);

        var body = Assert.IsType<BlockSyntax>(forStatement.Body);
        Assert.Empty(body.Statements);
    }

    [Fact]
    public void Parse_ForStatement_NoClauses()
    {
        var statements = Parse("for ;; {}");

        var forStatement = Assert.IsType<ForStatement>(statements[0]);
        Assert.Null(forStatement.Initializer);
        Assert.Null(forStatement.Condition);
        Assert.Null(forStatement.Iteration);
    }

    [Fact]
    public void Parse_ForStatement_OnlyCondition()
    {
        var statements = Parse("for ; x > 0; {}");

        var forStatement = Assert.IsType<ForStatement>(statements[0]);
        Assert.Null(forStatement.Initializer);
        Assert.NotNull(forStatement.Condition);
        Assert.Null(forStatement.Iteration);
    }

    [Fact]
    public void Parse_ForStatement_SingleStatementBody()
    {
        var statements = Parse("for ;; : break;");

        var forStatement = Assert.IsType<ForStatement>(statements[0]);
        Assert.IsType<BreakStatement>(forStatement.Body);
    }
}
