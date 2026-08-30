using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Lexing;

public sealed partial class Lexer
{
    private SyntaxToken LexIdentifier()
    {
        var start = _position;
        while (!IsAtEnd && (char.IsAsciiLetterOrDigit(Current) || Current == '_'))
        {
            Advance();
        }
        var length = _position - start;
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
            "return" => SyntaxTokenKind.Return,
            "if" => SyntaxTokenKind.If,
            "else" => SyntaxTokenKind.Else,
            "while" => SyntaxTokenKind.While,
            "for" => SyntaxTokenKind.For,
            "defer" => SyntaxTokenKind.Defer,
            "switch" => SyntaxTokenKind.Switch,
            "case" => SyntaxTokenKind.Case,
            "default" => SyntaxTokenKind.Default,
            "match" => SyntaxTokenKind.Match,
            "break" => SyntaxTokenKind.Break,
            "continue" => SyntaxTokenKind.Continue,
            "alias" => SyntaxTokenKind.Alias,
            "distinct" => SyntaxTokenKind.Distinct,
            "enum" => SyntaxTokenKind.Enum,
            "struct" => SyntaxTokenKind.Struct,
            "union" => SyntaxTokenKind.Union,

            "u8" => SyntaxTokenKind.U8,
            "u16" => SyntaxTokenKind.U16,
            "u32" => SyntaxTokenKind.U32,
            "u64" => SyntaxTokenKind.U64,
            "i8" => SyntaxTokenKind.I8,
            "i16" => SyntaxTokenKind.I16,
            "i32" => SyntaxTokenKind.I32,
            "i64" => SyntaxTokenKind.I64,
            "f32" => SyntaxTokenKind.F32,
            "f64" => SyntaxTokenKind.F64,
            "void" => SyntaxTokenKind.Void,
            "bool" => SyntaxTokenKind.Bool,
            "string" => SyntaxTokenKind.String,

            "true" => SyntaxTokenKind.True,
            "false" => SyntaxTokenKind.False,
            "null" => SyntaxTokenKind.Null,

            "and" => SyntaxTokenKind.And,
            "or" => SyntaxTokenKind.Or,

            _ => SyntaxTokenKind.Identifier
        };
    }
}