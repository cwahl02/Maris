using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Expression
{
    private static ExpressionSyntax ParseFirstExpression(string text)
    {
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();
        var expressionStatement = Assert.IsType<ExpressionStatement>(statements[0]);
        return Assert.Single(expressionStatement.Expressions.Elements);
    }

    [Fact]
    public void Parse_IntegerLiteral()
    {
        var expression = ParseFirstExpression("42;");
        var literal = Assert.IsType<LiteralExpression>(expression);
        Assert.Equal(SyntaxTokenKind.IntegerLiteral, literal.LiteralToken.Kind);
    }

    [Fact]
    public void Parse_Identifier()
    {
        var expression = ParseFirstExpression("foo;");
        var identifier = Assert.IsType<IdentifierExpression>(expression);
        Assert.Single(identifier.IdentifierTokens.Elements);
    }

    [Fact]
    public void Parse_Assignment_IsRightAssociative()
    {
        var expression = ParseFirstExpression("x = y = 1;");
        var assignment = Assert.IsType<AssignmentExpression>(expression);
        Assert.IsType<IdentifierExpression>(assignment.Left);
        Assert.IsType<AssignmentExpression>(assignment.Right);
    }

    [Fact]
    public void Parse_LogicalOr()
    {
        var expression = ParseFirstExpression("x or y;");
        var binary = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(SyntaxTokenKind.Or, binary.OperatorToken.Kind);
    }

    [Fact]
    public void Parse_LogicalAnd()
    {
        var expression = ParseFirstExpression("x and y;");
        var binary = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(SyntaxTokenKind.And, binary.OperatorToken.Kind);
    }

    [Fact]
    public void Parse_LogicalOr_PrecedesLogicalAnd()
    {
        // 'and' binds tighter than 'or', so this should parse as: x or (y and z)
        var expression = ParseFirstExpression("x or y and z;");
        var binary = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(SyntaxTokenKind.Or, binary.OperatorToken.Kind);
        Assert.IsType<IdentifierExpression>(binary.Left);
        var right = Assert.IsType<BinaryExpression>(binary.Right);
        Assert.Equal(SyntaxTokenKind.And, right.OperatorToken.Kind);
    }

    [Theory]
    [InlineData("x == y;", SyntaxTokenKind.EqualEqual)]
    [InlineData("x != y;", SyntaxTokenKind.BangEqual)]
    public void Parse_EqualityExpression(string text, SyntaxTokenKind expectedKind)
    {
        var expression = ParseFirstExpression(text);
        var binary = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(expectedKind, binary.OperatorToken.Kind);
    }

    [Theory]
    [InlineData("x < y;", SyntaxTokenKind.LessThan)]
    [InlineData("x <= y;", SyntaxTokenKind.LessThanEqual)]
    [InlineData("x > y;", SyntaxTokenKind.GreaterThan)]
    [InlineData("x >= y;", SyntaxTokenKind.GreaterThanEqual)]
    public void Parse_ComparisonExpression(string text, SyntaxTokenKind expectedKind)
    {
        var expression = ParseFirstExpression(text);
        var binary = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(expectedKind, binary.OperatorToken.Kind);
    }

    [Fact]
    public void Parse_TermExpression_PrecedesComparison()
    {
        // Term (+) should bind tighter than comparison, so: 1 + 2 < 4 => (1 + 2) < 4
        var expression = ParseFirstExpression("1 + 2 < 4;");
        var comparison = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(SyntaxTokenKind.LessThan, comparison.OperatorToken.Kind);
        var left = Assert.IsType<BinaryExpression>(comparison.Left);
        Assert.Equal(SyntaxTokenKind.Plus, left.OperatorToken.Kind);
    }

    [Fact]
    public void Parse_FactorExpression_PrecedesTerm()
    {
        // Factor (*) should bind tighter than term (+), so: 1 + 2 * 3 => 1 + (2 * 3)
        var expression = ParseFirstExpression("1 + 2 * 3;");
        var term = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(SyntaxTokenKind.Plus, term.OperatorToken.Kind);
        Assert.IsType<LiteralExpression>(term.Left);
        var right = Assert.IsType<BinaryExpression>(term.Right);
        Assert.Equal(SyntaxTokenKind.Star, right.OperatorToken.Kind);
    }

    [Theory]
    [InlineData("!x;", SyntaxTokenKind.Bang)]
    [InlineData("y = -x;", SyntaxTokenKind.Minus)]
    [InlineData("&x;", SyntaxTokenKind.Ampersand)]
    [InlineData("*x;", SyntaxTokenKind.Star)]
    public void Parse_UnaryExpression(string text, SyntaxTokenKind expectedOperator)
    {
        var expression = ParseFirstExpression(text);

        // A leading '-' at the start of a statement is consumed as a declaration
        // accessibility marker, so exercise unary minus via an assignment instead.
        if (expression is AssignmentExpression assignment)
        {
            expression = assignment.Right;
        }

        var unary = Assert.IsType<UnaryExpression>(expression);
        Assert.Equal(expectedOperator, unary.OperatorToken.Kind);
        Assert.IsType<IdentifierExpression>(unary.Operand);
    }

    [Fact]
    public void Parse_ParenthesizedExpression()
    {
        // Parentheses should override precedence: (1 + 2) * 3
        var expression = ParseFirstExpression("(1 + 2) * 3;");
        var factor = Assert.IsType<BinaryExpression>(expression);
        Assert.Equal(SyntaxTokenKind.Star, factor.OperatorToken.Kind);
        var parenthesized = Assert.IsType<ParenthesizedExpression>(factor.Left);
        Assert.IsType<BinaryExpression>(parenthesized.Expression);
    }

    [Theory]
    [InlineData("true;")]
    [InlineData("false;")]
    public void Parse_BooleanLiteral(string text)
    {
        var expression = ParseFirstExpression(text);
        var literal = Assert.IsType<LiteralExpression>(expression);
        Assert.True(literal.LiteralToken.Kind is SyntaxTokenKind.True or SyntaxTokenKind.False);
    }
}
