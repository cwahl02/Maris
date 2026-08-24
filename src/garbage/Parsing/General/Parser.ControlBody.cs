namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode? ParseControlBody()
    {
        if (_iterator.Current.Kind == Lexing.TokenKind.Dot)
        {
            Match(Lexing.TokenKind.Dot);
            return ParseStatement();
        }
        else if (_iterator.Current.Kind == Lexing.TokenKind.LeftBrace)
        {
            return ParseBlockStatement();
        }
        else
        {
            return null;
        }
    }
}