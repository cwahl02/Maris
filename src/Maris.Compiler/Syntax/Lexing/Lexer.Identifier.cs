using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexIdentifier()
    {
        var start = _iterator.Position;
        while (!_iterator.IsAtEnd && (char.IsAsciiLetterOrDigit(_iterator.Current) || _iterator.Current == '_'))
        {
            _iterator.Forward();
        }
        var length = _iterator.Position - start;
        var text = _sourceFile.Text.Substring(start, length);
        return new SyntaxToken(IsKeyword(text), start, length);
    }

    private static SyntaxTokenKind IsKeyword(string text)
    {
        return text switch
        {
            "import" => SyntaxTokenKind.Import,
            "module" => SyntaxTokenKind.Module,
            "as" => SyntaxTokenKind.As,
            _ => SyntaxTokenKind.Identifier
        };
    }
}