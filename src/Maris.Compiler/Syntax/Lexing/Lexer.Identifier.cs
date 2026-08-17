using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private Token LexIdentifier()
    {
        var start = _iterator.Position;
        while (!_iterator.IsAtEnd && (char.IsAsciiLetterOrDigit(_iterator.Current) || _iterator.Current == '_'))
        {
            _iterator.Forward();
        }
        var length = _iterator.Position - start;
        var text = _sourceFile.Text.Substring(start, length);
        return new Token(IsKeyword(text), start, length);
    }

    private static TokenKind IsKeyword(string text)
    {
        return text switch
        {
            "import" => TokenKind.Import,
            "module" => TokenKind.Module,
            "as" => TokenKind.As,
            _ => TokenKind.Identifier
        };
    }
}