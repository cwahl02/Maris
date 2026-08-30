namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexPipe()
    {
        var start = _position;
        if (Match("||"))
        {
            return new SyntaxToken(SyntaxTokenKind.PipePipe, start, 2);
        }
        else if (Match("|="))
        {
            return new SyntaxToken(SyntaxTokenKind.PipeEqual, start, 2);
        }
        else
        {
            Advance();
            return new SyntaxToken(SyntaxTokenKind.Pipe, start, 1);
        }
    }
}