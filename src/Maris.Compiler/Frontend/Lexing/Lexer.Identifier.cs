namespace Maris.Compiler.Lexing;

using Maris.Core.Text;

public sealed partial class Lexer
{
    private Token LexIdentifier()
    {
        var start = Position;
        while (!IsAtEnd && IsIdentifierPart(Current))
        {
            Advance();
        }

        var value = _text.AsSpan(start, Position - start);
        var type = IsKeyword(value);
        return MakeToken(type, start, Position - start);
    }

    private static bool IsIdentifierStart(char c) => char.IsLetter(c) || c == '_';
    private static bool IsIdentifierPart(char c) => char.IsLetterOrDigit(c) || c == '_';

    private static TokenType IsKeyword(ReadOnlySpan<char> value)
    {
        return value switch
        {
            "if" => TokenType.If,
            "else" => TokenType.Else,
            "continue" => TokenType.Continue,
            "break" => TokenType.Break,
            "return" => TokenType.Return,
            "switch" => TokenType.Switch,
            "case" => TokenType.Case,
            "default" => TokenType.Default,
            "defer" => TokenType.Defer,
            "match" => TokenType.Match,

            "while" => TokenType.While,
            "for" => TokenType.For,
            "foreach" => TokenType.Foreach,

            "u8" => TokenType.U8,
            "u16" => TokenType.U16,
            "u32" => TokenType.U32,
            "u64" => TokenType.U64,

            "i8" => TokenType.I8,
            "i16" => TokenType.I16,
            "i32" => TokenType.I32,
            "i64" => TokenType.I64,

            "f32" => TokenType.F32,
            "f64" => TokenType.F64,

            "void" => TokenType.Void,
            "bool" => TokenType.Bool,
            "string" => TokenType.String,

            "alias" => TokenType.Alias,
            "distinct" => TokenType.Distinct,
            "enum" => TokenType.Enum,
            "struct" => TokenType.Struct,
            "union" => TokenType.Union,

            "import" => TokenType.Import,
            "module" => TokenType.Module,
            "as" => TokenType.As,
            "foreign" => TokenType.Foreign,
            "null" => TokenType.Null,
            "sizeof" => TokenType.Sizeof,
            "typeof" => TokenType.Typeof,
            "move" => TokenType.Move,
            _ => TokenType.Identifier
        };
    }
}