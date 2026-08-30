namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexPipe()
    {
        var start = _iterator.Position;
        if (TryMatch("||"))
        {
            return new SyntaxToken(SyntaxTokenKind.PipePipe, start, 2);
        }
        else if (TryMatch("|="))
        {
            return new SyntaxToken(SyntaxTokenKind.PipeEqual, start, 2);
        }
        else
        {
            _iterator.Forward();
            return new SyntaxToken(SyntaxTokenKind.Pipe, start, 1);
        }
    }
}