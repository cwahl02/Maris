namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexPipe()
    {
        var start = _iterator.Position;
        if (TryMatch("||"))
        {
            return new Token(TokenKind.PipePipe, start, 2);
        }
        else if (TryMatch("|="))
        {
            return new Token(TokenKind.PipeEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new Token(TokenKind.Pipe, start, 1);
        }
    }
}