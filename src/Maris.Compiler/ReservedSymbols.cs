using System.Diagnostics;
using Maris.Compiler.Lexing;

namespace Maris.Compiler;

public static class ReservedSymbols
{
    public static TokenType GetReservedSymbol(ReadOnlySpan<char> text)
    {
        return text switch
        {  
            // Control Flow
            "if" => TokenType.If,
            "else" => TokenType.Else,
            "return" => TokenType.Return,
            "switch" => TokenType.Switch,
            "case" => TokenType.Case,
            "default" => TokenType.Default,
            "break" => TokenType.Break,
            "continue" => TokenType.Continue,
            "match" => TokenType.Match,
            "defer" => TokenType.Defer,

            // Loops
            "while" => TokenType.While,
            "for" => TokenType.For,
            "foreach" => TokenType.Foreach,

            // Types
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

            // User-defined types
            "alias" => TokenType.Alias,
            "distinct" => TokenType.Distinct,
            "enum" => TokenType.Enum,
            "struct" => TokenType.Struct,
            "union" => TokenType.Union,

            // Modules
            "import" => TokenType.Import,
            "module" => TokenType.Module,
            "as" => TokenType.As,            
            
            _ => TokenType.Identifier
        };
    }
}