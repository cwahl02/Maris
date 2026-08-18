using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class Identifier
{
    [Fact]
    public void Lex_Identifier()
    {
        var text = "myIdentifier";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "myIdentifier"));
        Assert.True(tokens.Contains(TokenKind.Identifier, TokenKind.Eof));
    }

    [Fact]
    public void Lex_Keywords()
    {
        var text = "myIdent if else while for return defer continue break switch case default";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "myIdent"));
        Assert.True(tokens.Contains(
            TokenKind.Identifier,
            TokenKind.If,
            TokenKind.Else,
            TokenKind.While,
            TokenKind.For,
            TokenKind.Return,
            TokenKind.Defer,
            TokenKind.Continue,
            TokenKind.Break,
            TokenKind.Switch,
            TokenKind.Case,
            TokenKind.Default,
            TokenKind.Eof
        ));
    }

    [Fact]
    public void Lex_PrimitiveType_Keywords()
    {
        var text = "u8 u16 u32 u64 i8 i16 i32 i64 f32 f64 void bool string";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "u8", "u16", "u32", "u64", "i8", "i16", "i32", "i64", "f32", "f64", "void", "bool", "string"));
        Assert.True(tokens.Contains(
            TokenKind.U8,
            TokenKind.U16,
            TokenKind.U32,
            TokenKind.U64,

            TokenKind.I8,
            TokenKind.I16,
            TokenKind.I32,
            TokenKind.I64,

            TokenKind.F32,
            TokenKind.F64,

            TokenKind.Void,
            TokenKind.Bool,
            TokenKind.String,
            TokenKind.Eof
        ));
    }

    [Fact]
    public void Lex_UserDefinedType_Keywords()
    {
        var text = "alias distinct enum struct union";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "alias", "distinct", "enum", "struct", "union"));
        Assert.True(tokens.Contains(
            TokenKind.Alias,
            TokenKind.Distinct,
            TokenKind.Enum,
            TokenKind.Struct,
            TokenKind.Union,
            TokenKind.Eof
        ));
    }

    [Fact]
    public void Lex_ControlFlow_Keywords()
    {
        var text = "if else while for return defer continue break switch case default";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "if", "else", "while", "for", "return", "defer", "continue", "break", "switch", "case", "default"));
        Assert.True(tokens.Contains(
            TokenKind.If,
            TokenKind.Else,
            TokenKind.While,
            TokenKind.For,
            TokenKind.Return,
            TokenKind.Defer,
            TokenKind.Continue,
            TokenKind.Break,
            TokenKind.Switch,
            TokenKind.Case,
            TokenKind.Default,
            TokenKind.Eof
        ));
    }

    [Fact]
    public void Lex_Module_Keywords()
    {
        var text = "module import as";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(text, "module", "import", "as"));
        Assert.True(tokens.Contains(
            TokenKind.Module,
            TokenKind.Import,
            TokenKind.As,
            TokenKind.Eof
        ));
    }
}