using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Module
{
    [Fact]
    public void Parse_ModuleDeclaration()
    {
        var text = "module MyModule;";
        var sourceFile = new SourceFile("", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex();
        var parser = new Parser(tokens);
        var compilationUnitSyntax = parser.ParseCompilationUnit();

        var moduleDeclaration = compilationUnitSyntax.Items[0] as ModuleDeclarationSyntax;
        Assert.NotNull(moduleDeclaration);
        Assert.Equal(TokenKind.Identifier, moduleDeclaration.QualifiedName[0].Kind);
    }
}