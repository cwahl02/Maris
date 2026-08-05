using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    [Fact]
    public void Lex_Identifier()
    {
        var source = "myIdentifier";
        var lexer = new Lexer(source);
        var tokens = lexer.Lex();

        Equal(
            source,
            (TokenType.Identifier, "myIdentifier"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_Keywords()
    {
        Equal(
            "myIdent if else while for return defer continue break switch case default",
            (TokenType.Identifier, "myIdent"),
            (TokenType.If, "if"),
            (TokenType.Else, "else"),
            (TokenType.While, "while"),
            (TokenType.For, "for"),
            (TokenType.Return, "return"),
            (TokenType.Defer, "defer"),
            (TokenType.Continue, "continue"),
            (TokenType.Break, "break"),
            (TokenType.Switch, "switch"),
            (TokenType.Case, "case"),
            (TokenType.Default, "default"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_PrimitiveType_Keywords()
    {
        Equal(
            "u8 u16 u32 u64 i8 i16 i32 i64 f32 f64 void bool string",
            (TokenType.U8, "u8"),
            (TokenType.U16, "u16"),
            (TokenType.U32, "u32"),
            (TokenType.U64, "u64"),

            (TokenType.I8, "i8"),
            (TokenType.I16, "i16"),
            (TokenType.I32, "i32"),
            (TokenType.I64, "i64"),

            (TokenType.F32, "f32"),
            (TokenType.F64, "f64"),

            (TokenType.Void, "void"),
            (TokenType.Bool, "bool"),
            (TokenType.String, "string"),
            (TokenType.EOF, "")
        );
    }

    [Fact]
    public void Lex_UserDefinedType_Keywords()
    {
        Equal(
            "alias distinct enum struct union",
            (TokenType.Alias, "alias"),
            (TokenType.Distinct, "distinct"),
            (TokenType.Enum, "enum"),
            (TokenType.Struct, "struct"),
            (TokenType.Union, "union"),
            (TokenType.EOF, "")
        );
    }

    // TODO: Re-enable this test once we have a way to lex collection types. This will require some changes to the lexer, as it currently does not support lexing collection types like `[]` and `[1]` correctly.
    // [Fact]
    // public void Lex_CollectionType_Keywords()
    // {
    //     Equal(
    //         "[] [1]",
    //         (TokenType.Slice, "[]"),
    //         (TokenType.Array, "[1]"),
    //         (TokenType.EOF, "")
    //     );
    // }
}