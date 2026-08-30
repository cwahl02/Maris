using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class While
{
    [Fact]
    public void Parse_WhileStatement_Block()
    {
        var text = "while x > 0 {}";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);

        var whileStatement = Assert.IsType<WhileStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.While, whileStatement.WhileKeyword.Kind);

        var condition = Assert.IsType<BinaryExpression>(whileStatement.Condition);
        Assert.Equal(SyntaxTokenKind.GreaterThan, condition.OperatorToken.Kind);

        var body = Assert.IsType<BlockSyntax>(whileStatement.Body);
        Assert.Empty(body.Statements);
    }

    [Fact]
    public void Parse_WhileStatement_Single()
    {
        var text = "while x > 0: break;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);

        var whileStatement = Assert.IsType<WhileStatement>(statements[0]);
        Assert.IsType<BreakStatement>(whileStatement.Body);
    }
}
