using Maris.Compiler.Syntax.Lexing;
using Maris.Compiler.Syntax.Parsing;
using Maris.Core.Text;

namespace Maris.Compiler.Syntax.Parsing;

public class Import
{
    [Fact]
    public void Parse_ImportDeclaration()
    {
        var text = "import TestModule;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var importDeclaration = Assert.IsType<ImportDeclaration>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Import, importDeclaration.ImportKeyword.Kind);

        var path = Assert.IsType<SeparatedSyntax<TokenSyntax>>(importDeclaration.Path);
        Assert.Single(path.Elements);
        Assert.Equal("TestModule", text.Substring(path.Elements[0].Token.Span.Start, path.Elements[0].Token.Span.Length));
    }

    [Fact]
    public void Parse_ImportDeclaration_WithAsAlias()
    {
        var text = "import TestModule as TM;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var importDeclaration = Assert.IsType<ImportDeclaration>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Import, importDeclaration.ImportKeyword.Kind);

        var path = Assert.IsType<SeparatedSyntax<TokenSyntax>>(importDeclaration.Path);
        Assert.Single(path.Elements);
        Assert.Equal("TestModule", text.Substring(path.Elements[0].Token.Span.Start, path.Elements[0].Token.Span.Length));

        Assert.Equal(SyntaxTokenKind.As, importDeclaration.AsKeyword?.Kind);
        Assert.Equal("TM", text.Substring(importDeclaration.Alias!.Token.Span.Start, importDeclaration.Alias.Token.Span.Length));
    }

    [Fact]
    public void Parse_ImportDeclaration_Path()
    {
        var text = "import TestModule.SubModule;";
        var sourceFile = new SourceFile("TestModule.maris", text);
        var lexer = new Lexer(sourceFile);
        var tokens = lexer.Lex().ToList();
        var parser = new Parser(tokens);
        var statements = parser.Parse();

        Assert.NotNull(statements);
        
        var importDeclaration = Assert.IsType<ImportDeclaration>(statements[0]);
        Assert.Equal(SyntaxTokenKind.Import, importDeclaration.ImportKeyword.Kind);

        var path = Assert.IsType<SeparatedSyntax<TokenSyntax>>(importDeclaration.Path);
        Assert.Equal(2, path.Elements.Count());
        Assert.Equal("TestModule", text.Substring(path.Elements[0].Token.Span.Start, path.Elements[0].Token.Span.Length));
        Assert.Equal("SubModule", text.Substring(path.Elements[1].Token.Span.Start, path.Elements[1].Token.Span.Length));
    }
}