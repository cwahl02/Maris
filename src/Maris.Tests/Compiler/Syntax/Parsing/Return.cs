using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Return
{
    [Fact]
    public void Parse_ReturnDeclaration()
    {
        var text = "main :: () -> int { return 42; }";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);

        var declarations = parser.Parse();

        Assert.NotNull(declarations);
        
        var returnStatement = Assert.IsType<ReturnSyntax>(declarations[0]);
        Assert.Equal(SyntaxTokenKind.Return, returnStatement.ReturnKeyword.Kind);

        var expression = Assert.IsType<ExpressionListSyntax>(returnStatement.Expressions);
        var literalExpression = Assert.IsType<LiteralExpressionSyntax>(expression.Expressions[0]);
        Assert.Equal("42", text.Substring(literalExpression.LiteralToken.Span.Start, literalExpression.LiteralToken.Span.Length));
    }
}