using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private Token Match(TokenKind expected)
    {
        if (_iterator.Current.Kind == expected)
        {
            var token = _iterator.Current;
            _iterator.Forward();
            return token;
        }
        
        return CreateMissingToken(expected, _iterator.Current.Kind);
    }

    private Token CreateMissingToken(TokenKind expected, TokenKind current)
    {
        return new Token(
            expected,
            _iterator.Current.Span.Start,
            0
        );
    }
}