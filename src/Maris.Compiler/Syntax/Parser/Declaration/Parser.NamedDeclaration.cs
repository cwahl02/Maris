using Maris.Compiler.Syntax.Lexing;

namespace Maris.Compiler.Syntax.Parsing;

public sealed partial class Parser
{
    private DeclarationSyntax ParseNamedDeclaration()
    {
        return _iterator.Current.Kind switch
        {
            SyntaxTokenKind.LeftParen => ParseFunction(),
            SyntaxTokenKind.Alias => ParseAlias(),
            SyntaxTokenKind.Distinct => ParseDistinct(),
            SyntaxTokenKind.Enum => ParseEnum(),
            SyntaxTokenKind.Struct => ParseStruct(),
            SyntaxTokenKind.Union => ParseUnion(),
        };
    }
}