using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;


namespace Maris.Tests.Compiler.Syntax.Lexing;

public class HelloWorld
{
    [Fact]
    public void Lex_Program_HelloWorld()
    {
        var text =
            """
            import std.io;

            main :: () -> i32 {
                print("Hello, World!");
                return 0;
            }
            """
        ;
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();

        Assert.True(tokens.Contains(SyntaxTokenKind.Invalid) == false, "Tokens should not contain any Invalid tokens.");
        Assert.True(tokens.Contains(text, "import", "std", "io", "main", "print", "return"));
    }
}