using Maris.Compiler.Lexing;

namespace Maris.Tests.Compiler.Lexing;

public class Identifier
{
    [Fact]
    public void Lex_Identifier()
    {
        var lexer = new Lexer("myIdentifier");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "myIdentifier");
        LexerAssert.ContainsTokenTypes(tokens, TokenType.Identifier, TokenType.EOF);
    }

    [Fact]
    public void Lex_Keywords()
    {
        var lexer = new Lexer("myIdent if else while for return defer continue break switch case default");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "myIdent");
        LexerAssert.ContainsTokenTypes(
            tokens,
            TokenType.Identifier,
            TokenType.If,
            TokenType.Else,
            TokenType.While,
            TokenType.For,
            TokenType.Return,
            TokenType.Defer,
            TokenType.Continue,
            TokenType.Break,
            TokenType.Switch,
            TokenType.Case,
            TokenType.Default,
            TokenType.EOF
        );
    }

    [Fact]
    public void Lex_PrimitiveType_Keywords()
    {
        var lexer = new Lexer("u8 u16 u32 u64 i8 i16 i32 i64 f32 f64 void bool string");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "u8", "u16", "u32", "u64", "i8", "i16", "i32", "i64", "f32", "f64", "void", "bool", "string");
        LexerAssert.ContainsTokenTypes(
            tokens,
            TokenType.U8,
            TokenType.U16,
            TokenType.U32,
            TokenType.U64,

            TokenType.I8,
            TokenType.I16,
            TokenType.I32,
            TokenType.I64,

            TokenType.F32,
            TokenType.F64,

            TokenType.Void,
            TokenType.Bool,
            TokenType.String,
            TokenType.EOF
        );
    }

    [Fact]
    public void Lex_UserDefinedType_Keywords()
    {
        var lexer = new Lexer("alias distinct enum struct union");
        var tokens = lexer.Lex();

        LexerAssert.ContainsText(tokens, "alias", "distinct", "enum", "struct", "union");
        LexerAssert.ContainsTokenTypes(
            tokens,
            TokenType.Alias,
            TokenType.Distinct,
            TokenType.Enum,
            TokenType.Struct,
            TokenType.Union,
            TokenType.EOF
        );
    }
}