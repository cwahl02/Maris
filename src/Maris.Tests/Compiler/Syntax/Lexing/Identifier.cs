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
        Assert.True(tokens.Contains(SyntaxTokenKind.Identifier, SyntaxTokenKind.Eof));
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
            SyntaxTokenKind.Identifier,
            SyntaxTokenKind.If,
            SyntaxTokenKind.Else,
            SyntaxTokenKind.While,
            SyntaxTokenKind.For,
            SyntaxTokenKind.Return,
            SyntaxTokenKind.Defer,
            SyntaxTokenKind.Continue,
            SyntaxTokenKind.Break,
            SyntaxTokenKind.Switch,
            SyntaxTokenKind.Case,
            SyntaxTokenKind.Default,
            SyntaxTokenKind.Eof
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
            SyntaxTokenKind.U8,
            SyntaxTokenKind.U16,
            SyntaxTokenKind.U32,
            SyntaxTokenKind.U64,

            SyntaxTokenKind.I8,
            SyntaxTokenKind.I16,
            SyntaxTokenKind.I32,
            SyntaxTokenKind.I64,

            SyntaxTokenKind.F32,
            SyntaxTokenKind.F64,

            SyntaxTokenKind.Void,
            SyntaxTokenKind.Bool,
            SyntaxTokenKind.String,
            SyntaxTokenKind.Eof
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
            SyntaxTokenKind.Alias,
            SyntaxTokenKind.Distinct,
            SyntaxTokenKind.Enum,
            SyntaxTokenKind.Struct,
            SyntaxTokenKind.Union,
            SyntaxTokenKind.Eof
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
            SyntaxTokenKind.If,
            SyntaxTokenKind.Else,
            SyntaxTokenKind.While,
            SyntaxTokenKind.For,
            SyntaxTokenKind.Return,
            SyntaxTokenKind.Defer,
            SyntaxTokenKind.Continue,
            SyntaxTokenKind.Break,
            SyntaxTokenKind.Switch,
            SyntaxTokenKind.Case,
            SyntaxTokenKind.Default,
            SyntaxTokenKind.Eof
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
            SyntaxTokenKind.Module,
            SyntaxTokenKind.Import,
            SyntaxTokenKind.As,
            SyntaxTokenKind.Eof
        ));
    }
}