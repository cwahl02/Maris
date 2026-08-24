namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private SyntaxNode ParseStatement()
    {
        return _iterator.Current.Kind switch
        {
            Lexing.TokenKind.If => ParseIfStatement(),
            Lexing.TokenKind.While => ParseWhileStatement(),
            Lexing.TokenKind.For => ParseForStatement(),
            Lexing.TokenKind.Break => ParseBreakStatement(),
            Lexing.TokenKind.Continue => ParseContinueStatement(),
            Lexing.TokenKind.Return => ParseReturnStatement(),
            Lexing.TokenKind.LeftBrace => ParseBlockStatement(),
            Lexing.TokenKind.Semicolon => ParseEmptyStatement(),
            _ => throw new Exception($"Unexpected token: {_iterator.Current.Kind}"),
        };
    }
}