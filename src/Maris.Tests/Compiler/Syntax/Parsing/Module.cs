using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Module
{
    [Fact]
    public void Parse_ModuleDeclaration_Statement()
    {
        var text = "module TestModule;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);

        var declarations = parser.Parse();

        Assert.NotNull(declarations);
        
        var module = Assert.IsType<ModuleSyntax>(declarations[0]);
        Assert.Equal(SyntaxTokenKind.Module, module.ModuleKeyword.Kind);

        var path = Assert.IsType<IdentifierPathSyntax>(module.Path);
        Assert.Single(path.Identifiers);
        Assert.Equal("TestModule", text.Substring(path.Identifiers[0].Span.Start, path.Identifiers[0].Span.Length));
    }

    [Fact]
    public void Parse_ModuleDeclaration_Block()
    {
        var text = "module TestModule {}";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);

        var declarations = parser.Parse();

        Assert.NotNull(declarations);
        
        var module = Assert.IsType<ModuleSyntax>(declarations[0]);
        Assert.Equal(SyntaxTokenKind.Module, module.ModuleKeyword.Kind);

        var path = Assert.IsType<IdentifierPathSyntax>(module.Path);
        Assert.Single(path.Identifiers);
        Assert.Equal("TestModule", text.Substring(path.Identifiers[0].Span.Start, path.Identifiers[0].Span.Length));
    }
}