using Maris.Compiler.Lexer;

public partial class LexerTests
{
    [Fact]
    public void LexProgram_Descript_ShouldReturnTokens()
    {
        Equal(
            """
            import std.mem;
            import os.win as win;

            foreign "libc" {
                printf :: (format: *u8, value: i32) -> i32;
                malloc :: (size: u64) -> *void;
                free(ptr: *void) -> void;
            }

            Node :: struct {
                value: i32 = 0;
                next: *Next = null;
                linear_handle: !*u8 = null; // Linear ownership pointer
            }

            MAX_CONNECTIONS :: u64 = 1024;

            main :: () -> i32 {
                counter: i32 = 0;

                inferred_mut := 42;
                inferred_const ::= 99;

                matrix: *!*[10]Node = null;

                if counter == 0 {
                    io_print("Branch single-line or block match!");
                }
                else:
                    counter = counter + 1;

                raw_mem := malloc(Node.sizeof);
                node_ptr: *Node = raw_mem;

                node_ptr.value = 100;

                defer free(raw_mem);

                local_linear: !*u8 = move node_ptr.linear_handle;

                return 0;
            }
            """,
            // 'import std.mem;'
            (TokenType.Import, "import"),
            (TokenType.Identifier, "std"),
            (TokenType.Dot, "."),
            (TokenType.Identifier, "mem"),
            (TokenType.Semicolon, ";"),

            // 'import os.win as win;'
            (TokenType.Import, "import"),
            (TokenType.Identifier, "os"),
            (TokenType.Dot, "."),
            (TokenType.Identifier, "win"),
            (TokenType.As, "as"),
            (TokenType.Identifier, "win"),
            (TokenType.Semicolon, ";"),

            // 'foreign "libc" {'
            (TokenType.Foreign, "foreign"),
            (TokenType.StringLiteral, "libc"),
            (TokenType.LeftBrace, "{"),

            // 'printf(format: *u8, value: i32) -> i32;'
            (TokenType.Identifier, "printf"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "format"),
            (TokenType.Colon, ":"),
            (TokenType.Star, "*"),
            (TokenType.U8, "u8"),
            (TokenType.Comma, ","),
            (TokenType.Identifier, "value"),
            (TokenType.Colon, ":"),
            (TokenType.I32, "i32"),
            (TokenType.RightParen, ")"),
            (TokenType.Arrow, "->"),
            (TokenType.I32, "i32"),
            (TokenType.Semicolon, ";"),

            // 'malloc(size: u64) -> *void;'
            (TokenType.Identifier, "malloc"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "size"),
            (TokenType.Colon, ":"),
            (TokenType.U64, "u64"),
            (TokenType.RightParen, ")"),
            (TokenType.Arrow, "->"),
            (TokenType.Star, "*"),
            (TokenType.Void, "void"),
            (TokenType.Semicolon, ";"),

            // 'free(ptr: *void) -> void;'
            (TokenType.Identifier, "free"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "ptr"),
            (TokenType.Colon, ":"),
            (TokenType.Star, "*"),
            (TokenType.Void, "void"),
            (TokenType.RightParen, ")"),
            (TokenType.Arrow, "->"),
            (TokenType.Void, "void"),
            (TokenType.Semicolon, ";"),

            // '}'
            (TokenType.RightBrace, "}"),

            // 'Node :: struct {'
            (TokenType.Identifier, "Node"),
            (TokenType.ColonColon, "::"),
            (TokenType.Struct, "struct"),
            (TokenType.LeftBrace, "{"),

            // 'value: i32 = 0;'
            (TokenType.Identifier, "value"),
            (TokenType.Colon, ":"),
            (TokenType.I32, "i32"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "0"),
            (TokenType.Semicolon, ";"),

            // 'next: *Node = null;'
            (TokenType.Identifier, "next"),
            (TokenType.Colon, ":"),
            (TokenType.Star, "*"),
            (TokenType.Identifier, "Node"),
            (TokenType.Equal, "="),
            (TokenType.Null, "null"),
            (TokenType.Semicolon, ";"),

            // 'linear_handle: !*u8 = null;'
            (TokenType.Identifier, "linear_handle"),
            (TokenType.Colon, ":"),
            (TokenType.Bang, "!"),
            (TokenType.Star, "*"),
            (TokenType.U8, "u8"),
            (TokenType.Equal, "="),
            (TokenType.Null, "null"),
            (TokenType.Semicolon, ";"),

            // '}'
            (TokenType.RightBrace, "}"),

            // 'MAX_CONNECTIONS :: u64 = 1024;'
            (TokenType.Identifier, "MAX_CONNECTIONS"),
            (TokenType.ColonColon, "::"),
            (TokenType.U64, "u64"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "1024"),
            (TokenType.Semicolon, ";"),

            // 'main :: () -> i32 {'
            (TokenType.Identifier, "main"),
            (TokenType.ColonColon, "::"),
            (TokenType.LeftParen, "("),
            (TokenType.RightParen, ")"),
            (TokenType.Arrow, "->"),
            (TokenType.I32, "i32"),
            (TokenType.LeftBrace, "{"),

            // 'counter: i32 = 0;'
            (TokenType.Identifier, "counter"),
            (TokenType.Colon, ":"),
            (TokenType.I32, "i32"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "0"),
            (TokenType.Semicolon, ";"),

            // 'inferred_mut := 42;'
            (TokenType.Identifier, "inferred_mut"),
            (TokenType.ColonEqual, ":="),
            (TokenType.IntegerLiteral, "42"),
            (TokenType.Semicolon, ";"),

            // 'inferred_const ::= 999;'
            (TokenType.Identifier, "inferred_const"),
            (TokenType.ColonColonEqual, "::="),
            (TokenType.IntegerLiteral, "999"),
            (TokenType.Semicolon, ";"),

            // 'matrix: *!*[10]Node = null;'
            (TokenType.Identifier, "matrix"),
            (TokenType.Colon, ":"),
            (TokenType.Star, "*"),
            (TokenType.Bang, "!"),
            (TokenType.Star, "*"),
            (TokenType.LeftBracket, "["),
            (TokenType.IntegerLiteral, "10"),
            (TokenType.RightBracket, "]"),
            (TokenType.Identifier, "Node"),
            (TokenType.Equal, "="),
            (TokenType.Null, "null"),
            (TokenType.Semicolon, ";"),

            // 'if counter == 0 {'
            (TokenType.If, "if"),
            (TokenType.Identifier, "counter"),
            (TokenType.EqualEqual, "=="),
            (TokenType.IntegerLiteral, "0"),
            (TokenType.LeftBrace, "{"),
            
            // 'io_print("Branch single-line or block match!");'
            (TokenType.Identifier, "io_print"),
            (TokenType.LeftParen, "("),
            (TokenType.StringLiteral, "\"Branch single-line or block match!\""),
            (TokenType.RightParen, ")"),
            (TokenType.Semicolon, ";"),
            
            // '} else:'
            (TokenType.RightBrace, "}"),
            (TokenType.Else, "else"),
            (TokenType.Colon, ":"),

            // 'counter = counter + 1;'
            (TokenType.Identifier, "counter"),
            (TokenType.Equal, "="),
            (TokenType.Identifier, "counter"),
            (TokenType.Plus, "+"),
            (TokenType.IntegerLiteral, "1"),
            (TokenType.Semicolon, ";"),

            // 'raw_mem := malloc(Node.sizeof);'
            (TokenType.Identifier, "raw_mem"),
            (TokenType.ColonEqual, ":="),
            (TokenType.Identifier, "malloc"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "Node"),
            (TokenType.Dot, "."),
            (TokenType.Sizeof, "sizeof"),
            (TokenType.RightParen, ")"),
            (TokenType.Semicolon, ";"),

            // 'node_ptr: *Node = raw_mem;'
            (TokenType.Identifier, "node_ptr"),
            (TokenType.Colon, ":"),
            (TokenType.Star, "*"),
            (TokenType.Identifier, "Node"),
            (TokenType.Equal, "="),
            (TokenType.Identifier, "raw_mem"),
            (TokenType.Semicolon, ";"),

            // 'node_ptr.value = 100;'
            (TokenType.Identifier, "node_ptr"),
            (TokenType.Dot, "."),
            (TokenType.Identifier, "value"),
            (TokenType.Equal, "="),
            (TokenType.IntegerLiteral, "100"),
            (TokenType.Semicolon, ";"),

            // 'defer free(raw_mem);'
            (TokenType.Defer, "defer"),
            (TokenType.Identifier, "free"),
            (TokenType.LeftParen, "("),
            (TokenType.Identifier, "raw_mem"),
            (TokenType.RightParen, ")"),
            (TokenType.Semicolon, ";"),

            // 'local_linear: !*u8 = move node_ptr.linear_handle;'
            (TokenType.Identifier, "local_linear"),
            (TokenType.Colon, ":"),
            (TokenType.Bang, "!"),
            (TokenType.Star, "*"),
            (TokenType.U8, "u8"),
            (TokenType.Equal, "="),
            (TokenType.Move, "move"),
            (TokenType.Identifier, "node_ptr"),
            (TokenType.Dot, "."),
            (TokenType.Identifier, "linear_handle"),
            (TokenType.Semicolon, ";"),

            // 'return 0;'
            (TokenType.Return, "return"),
            (TokenType.IntegerLiteral, "0"),
            (TokenType.Semicolon, ";"),

            // '}'
            (TokenType.RightBrace, "}")
        );
    }
}