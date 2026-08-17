namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexNumber()
    {
        var start = _iterator.Position;
        while (!_iterator.IsAtEnd && char.IsDigit(_iterator.Current))
        {
            _iterator.Forward();
        }
        return new Token(TokenKind.IntegerLiteral, start, _iterator.Position - start);
    }
}