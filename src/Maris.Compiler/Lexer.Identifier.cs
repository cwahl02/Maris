namespace Maris.Compiler;

public sealed partial class Lexer
{
    private Token LexIdentifier()
    {
        int start = _window.Position;

        _window.Advance();

        while (!_window.EndOfText() && (char.IsLetterOrDigit(_window.Current) || _window.Current == '_'))
            _window.Advance();

        return MakeToken(ReservedSymbols.GetReservedSymbol(_window.Slice(start, _window.Position - start)), start, _window.Position - start);
    }

    
}