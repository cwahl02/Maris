using Maris.Core.Iterator;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private readonly SourceFile _sourceFile;
    private readonly Iterator<char> _iterator;

    public Lexer(SourceFile sourceFile)
    {
        _sourceFile = sourceFile;
        _iterator = new Iterator<char>(sourceFile.Text.ToCharArray());
    }

    public List<Token> Lex()
    {
        List<Token> tokens = new();

        while (!_iterator.IsAtEnd)
        {
            tokens.Add(LexToken());
        }

        tokens.Add(Token.Eof);

        return tokens;
    }

    private Token LexToken()
    {
        if (char.IsAsciiLetter(_iterator.Current) || _iterator.Current == '_')
        {
            return LexIdentifier();
        }
        else if (char.IsDigit(_iterator.Current))
        {
            return LexNumber();
        }
        else if (_iterator.Current == '"')
        {
            return LexString();
        }
        else if (_iterator.Current == '\'')
        {
            return LexCharacter();
        }
        throw new NotImplementedException($"Lexing for character '{_iterator.Current}' is not implemented.");
        // else if (_iterator.Current == '"')
        // {
        //     return LexString();
        // }
        // else if (_iterator.Current == '\'')
        // {
        //     return LexCharacter();
        // }
        // else
        // {
        //     // Handle other token types or throw an error for unrecognized characters
        //     throw new NotImplementedException($"Lexing for character '{_iterator.Current}' is not implemented.");
        // }
    }

}