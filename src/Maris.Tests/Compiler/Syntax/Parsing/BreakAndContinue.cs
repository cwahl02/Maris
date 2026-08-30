using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class BreakAndContinue
{
    [Fact]
    public void Parse_BreakStatement()
    {
        var text = "break;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        var breakStatement = Assert.IsType<BreakStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Break, breakStatement.BreakKeyword.Kind);
        Assert.Equal(SyntaxTokenKind.Semicolon, breakStatement.Semicolon.Kind);
    }

    [Fact]
    public void Parse_ContinueStatement()
    {
        var text = "continue;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        var continueStatement = Assert.IsType<ContinueStatement>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Continue, continueStatement.ContinueKeyword.Kind);
        Assert.Equal(SyntaxTokenKind.Semicolon, continueStatement.Semicolon.Kind);
    }

    [Fact]
    public void Parse_BreakAndContinue_InsideBlock()
    {
        var text = "while true { break; continue; }";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        var whileStatement = Assert.IsType<WhileStatement>(statements[0]);
        var body = Assert.IsType<BlockSyntax>(whileStatement.Body);
        Assert.Equal(2, body.Statements.Count);
        Assert.IsType<BreakStatement>(body.Statements[0]);
        Assert.IsType<ContinueStatement>(body.Statements[1]);
    }
}
