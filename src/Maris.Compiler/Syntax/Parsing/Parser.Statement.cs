using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private StatementSyntax ParseStatement()
    {
        DeclarationAccessibility accessibility = ParseDeclarationAccessibility();

        return Current.Kind switch
        {
            // Declarations
            SyntaxTokenKind.Module => ParseModuleDeclaration(accessibility),
            SyntaxTokenKind.Import => ParseImportDeclaration(accessibility),
            SyntaxTokenKind.Identifier when IsNamedDeclarationStart() => ParseNamedDeclaration(accessibility),

            // Control flow
            SyntaxTokenKind.If => ParseIfStatement(),
            SyntaxTokenKind.While => ParseWhileStatement(),
            SyntaxTokenKind.For => ParseForStatement(),
            SyntaxTokenKind.Return => ParseReturnStatement(),
            SyntaxTokenKind.Break => ParseBreakStatement(),
            SyntaxTokenKind.Continue => ParseContinueStatement(),

            SyntaxTokenKind.LeftBrace => ParseBlock(),

            // Expressions
            _ => ParseExpressionStatement()
        };
    }

    // An identifier starts a named declaration when it is directly followed by a
    // binding token (':', '::', ':=' or '::='); otherwise it is an expression.
    private bool IsNamedDeclarationStart() => Peek(1).Kind is
        SyntaxTokenKind.Colon or
        SyntaxTokenKind.ColonColon or
        SyntaxTokenKind.ColonEqual or
        SyntaxTokenKind.ColonColonEqual;
}
