namespace Maris.Compiler.Parsing;

using Maris.Compiler.Lexing;

/// <summary>
/// A small recursive-descent parser for the subset of Maris syntax exercised by the
/// current sample programs and tests (imports, function declarations, simple
/// statements/expressions). It never throws on malformed input: syntax errors are
/// recorded as <see cref="ParseDiagnostic"/> entries and the parser recovers by
/// skipping ahead so it can keep consuming the remaining tokens.
/// </summary>
public sealed class Parser(List<Token> tokens)
{
    private readonly List<Token> _tokens = tokens;
    private int _position;
    public List<ParseDiagnostic> Diagnostics { get; } = [];

    private Token Current => _tokens[_position];
    private Token Previous => _tokens[_position - 1];
    private bool IsAtEnd => Current.Type == TokenType.EOF;

    public ProgramNode Parse()
    {
        var program = new ProgramNode();

        while (!IsAtEnd)
        {
            program.Declarations.Add(ParseDeclaration());
        }

        return program;
    }

    private SyntaxNode ParseDeclaration()
    {
        if (Check(TokenType.Import))
        {
            return ParseImportDeclaration();
        }

        if (Check(TokenType.Identifier) && CheckNext(TokenType.ColonColon))
        {
            return ParseFunctionDeclaration();
        }

        Diagnostics.Add(new ParseDiagnostic($"Unexpected token '{Current.Value}' at start of declaration.", Current.Start));
        return Synchronize();
    }

    private SyntaxNode ParseImportDeclaration()
    {
        Advance(); // 'import'
        var parts = new List<Token> { Expect(TokenType.Identifier, "Expected module name after 'import'.") };

        while (Match(TokenType.Dot))
        {
            parts.Add(Expect(TokenType.Identifier, "Expected identifier after '.' in import path."));
        }

        Expect(TokenType.Semicolon, "Expected ';' after import declaration.");
        return new ImportDeclarationNode(parts);
    }

    private SyntaxNode ParseFunctionDeclaration()
    {
        var name = Expect(TokenType.Identifier, "Expected function name.");
        Expect(TokenType.ColonColon, "Expected '::' after function name.");
        Expect(TokenType.LeftParen, "Expected '(' after '::'.");
        Expect(TokenType.RightParen, "Expected ')' to close parameter list.");
        Expect(TokenType.Arrow, "Expected '->' after parameter list.");
        var returnType = Advance();
        var body = ParseBlock();

        return new FunctionDeclarationNode(name, returnType, body);
    }

    private BlockNode ParseBlock()
    {
        var block = new BlockNode();
        Expect(TokenType.LeftBrace, "Expected '{' to start block.");

        while (!Check(TokenType.RightBrace) && !IsAtEnd)
        {
            block.Statements.Add(ParseStatement());
        }

        Expect(TokenType.RightBrace, "Expected '}' to close block.");
        return block;
    }

    private SyntaxNode ParseStatement()
    {
        if (Match(TokenType.Return))
        {
            SyntaxNode? expression = null;
            if (!Check(TokenType.Semicolon))
            {
                expression = ParseExpression();
            }

            Expect(TokenType.Semicolon, "Expected ';' after return statement.");
            return new ReturnStatementNode(expression);
        }

        var expr = ParseExpression();
        Expect(TokenType.Semicolon, "Expected ';' after expression statement.");
        return new ExpressionStatementNode(expr);
    }

    private SyntaxNode ParseExpression() => ParseCallOrPrimary();

    private SyntaxNode ParseCallOrPrimary()
    {
        var expression = ParsePrimary();

        while (Match(TokenType.LeftParen))
        {
            var arguments = new List<SyntaxNode>();
            if (!Check(TokenType.RightParen))
            {
                do
                {
                    arguments.Add(ParseExpression());
                } while (Match(TokenType.Comma));
            }

            Expect(TokenType.RightParen, "Expected ')' after arguments.");
            expression = new CallExpressionNode(expression, arguments);
        }

        return expression;
    }

    private SyntaxNode ParsePrimary()
    {
        if (Match(TokenType.Identifier))
        {
            return new IdentifierExpressionNode(Previous);
        }

        if (Match(TokenType.IntegerLiteral, TokenType.FloatLiteral, TokenType.StringLiteral, TokenType.CharacterLiteral))
        {
            return new LiteralExpressionNode(Previous);
        }

        Diagnostics.Add(new ParseDiagnostic($"Unexpected token '{Current.Value}' in expression.", Current.Start));
        var position = Current.Start;
        Advance();
        return new ErrorNode(position);
    }

    // Consumes tokens until a likely declaration boundary is found, so that a single
    // malformed declaration doesn't stop the rest of the file from being parsed.
    private SyntaxNode Synchronize()
    {
        var start = Current.Start;
        while (!IsAtEnd && !Check(TokenType.Semicolon) && !Check(TokenType.Import) &&
               !(Check(TokenType.Identifier) && CheckNext(TokenType.ColonColon)))
        {
            Advance();
        }

        if (Check(TokenType.Semicolon))
        {
            Advance();
        }

        return new ErrorNode(start);
    }

    private bool Check(TokenType type) => Current.Type == type;

    private bool CheckNext(TokenType type) =>
        _position + 1 < _tokens.Count && _tokens[_position + 1].Type == type;

    private Token Advance()
    {
        if (!IsAtEnd)
        {
            _position++;
        }

        return Previous;
    }

    private bool Match(params ReadOnlySpan<TokenType> types)
    {
        foreach (var type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }

        return false;
    }

    private Token Expect(TokenType type, string message)
    {
        if (Check(type))
        {
            return Advance();
        }

        Diagnostics.Add(new ParseDiagnostic(message, Current.Start));
        return Current;
    }
}
