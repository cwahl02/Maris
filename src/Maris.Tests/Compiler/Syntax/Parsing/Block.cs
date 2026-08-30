using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Block
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
    public void Parse_BlockStatement_Empty()
    {
        var statements = Parse("{}");

        var block = Assert.IsType<BlockSyntax>(statements[0]);
        Assert.Equal(SyntaxTokenKind.LeftBrace, block.LeftBrace.Kind);
        Assert.Equal(SyntaxTokenKind.RightBrace, block.RightBrace.Kind);
        Assert.Empty(block.Statements);
    }

    [Fact]
    public void Parse_BlockStatement_WithStatements()
    {
        var statements = Parse("{ return; break; }");

        var block = Assert.IsType<BlockSyntax>(statements[0]);
        Assert.Equal(2, block.Statements.Count);
        Assert.IsType<ReturnStatement>(block.Statements[0]);
        Assert.IsType<BreakStatement>(block.Statements[1]);
    }

    [Fact]
    public void Parse_BlockStatement_Nested()
    {
        var statements = Parse("{ { break; } }");

        var outer = Assert.IsType<BlockSyntax>(statements[0]);
        Assert.Single(outer.Statements);
        var inner = Assert.IsType<BlockSyntax>(outer.Statements[0]);
        Assert.Single(inner.Statements);
        Assert.IsType<BreakStatement>(inner.Statements[0]);
    }
}
