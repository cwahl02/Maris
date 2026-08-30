namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private FunctionDeclarationSyntax ParseFunctionDeclaration()
    {
        var identifier = Expect(Lexing.TokenKind.Identifier);
        var bindingOperator = default(Lexing.TokenKind);

        switch (_iterator.Current.Kind)
        {
            case Lexing.TokenKind.Colon:
            case Lexing.TokenKind.ColonColon:
            case Lexing.TokenKind.ColonEqual:
            case Lexing.TokenKind.ColonColonEqual:
                bindingOperator = _iterator.Current.Kind;
                _iterator.Forward();
                break;
            default:
                throw new Exception($"Unexpected token: {_iterator.Current.Kind}");
        }

        var parameters = ParseParameterList();
        var returnTypes = ParseReturnTypeList();
        var body = ParseBlock();

        return new FunctionDeclarationSyntax(
            identifier,
            bindingOperator,
            parameters,
            returnTypes,
            body
        );
    }

    private ParameterListSyntax ParseParameterList()
    {
        _iterator.Forward(); // Skip the opening parenthesis

        var groups = new List<ParameterGroupSyntax>();

        if( _iterator.Current.Kind != Lexing.TokenKind.CloseParen)
        {
            do
            {
                groups.Add(ParseParameterGroup());
            }
            while (_iterator.Current.Kind == Lexing.TokenKind.Comma)
            {
                _iterator.Forward(); // Skip the comma
            }
        }

        _iterator.Forward(); // Skip the closing parenthesis

        return new ParameterListSyntax(groups);
    }

    private ParameterGroupSyntax ParseParameterGroup()
    {
        var identifiers = ParseIdentifierList();
        _iterator.Forward(); // Skip the binding operator (colon or colon-colon)
        var type = ParseType();
        return new ParameterGroupSyntax(identifiers, type);
    }

    private IdentifierListSyntax ParseIdentifierList()
    {
        var identifier = new List<Lexing.Token>();

        do
        {
            identifier.Add(Expect(Lexing.TokenKind.Identifier));
        }
        while (_iterator.Current.Kind == Lexing.TokenKind.Comma)
        {
            _iterator.Forward(); // Skip the comma
        }

        return new IdentifierListSyntax(identifier);
    }
}

public sealed record FunctionDeclarationSyntax(
    Lexing.Token Identifier,
    Lexing.TokenKind BindingOperator,
    ParameterListSyntax Parameters,
    TypeListSyntax? ReturnTypes,
    BlockSyntax Body
) : DeclarationSyntax;