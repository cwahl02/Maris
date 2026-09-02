using Maris.Compiler.Lexing;
using Maris.Compiler.Parsing;

namespace Maris.Tests.Compiler.Parsing;

public class ParserTests
{
    [Fact]
    public void Parse_HelloWorldProgram_ProducesNoDiagnostics()
    {
        var tokens = new Lexer(
            """
            import std.io;

            main :: () -> i32 {
                print("Hello, World!");
                return 0;
            }
            """).Lex();

        var parser = new Parser(tokens);
        var program = parser.Parse();

        Assert.Empty(parser.Diagnostics);
        Assert.Equal(2, program.Declarations.Count);
        Assert.IsType<ImportDeclarationNode>(program.Declarations[0]);

        var function = Assert.IsType<FunctionDeclarationNode>(program.Declarations[1]);
        Assert.Equal("main", function.Name.Value.ToString());
        Assert.Equal("i32", function.ReturnType.Value.ToString());
        Assert.Equal(2, function.Body.Statements.Count);

        var call = Assert.IsType<ExpressionStatementNode>(function.Body.Statements[0]);
        Assert.IsType<CallExpressionNode>(call.Expression);

        var returnStatement = Assert.IsType<ReturnStatementNode>(function.Body.Statements[1]);
        Assert.NotNull(returnStatement.Expression);
    }

    [Fact]
    public void Parse_MalformedInput_DoesNotThrowAndRecordsDiagnostics()
    {
        var tokens = new Lexer("main :: ( -> { return; }").Lex();

        var parser = new Parser(tokens);
        var exception = Record.Exception(() => parser.Parse());

        Assert.Null(exception);
        Assert.NotEmpty(parser.Diagnostics);
    }

    [Fact]
    public void Parse_EmptyInput_ReturnsEmptyProgram()
    {
        var tokens = new Lexer(string.Empty).Lex();

        var parser = new Parser(tokens);
        var program = parser.Parse();

        Assert.Empty(program.Declarations);
        Assert.Empty(parser.Diagnostics);
    }
}
