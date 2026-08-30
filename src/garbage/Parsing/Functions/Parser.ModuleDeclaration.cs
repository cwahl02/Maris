// namespace Maris.Compiler.Syntax.Parsing;

// public sealed partial class Parser
// {
//     private SyntaxNode ParseModuleDeclaration()
//     {
//         var moduleKeyword = Expect(TokenKind.ModuleKeyword);
//         var identifier = ParseQualifiedName();

//         if (_iterator.Current.Kind == Lexing.TokenKind.Semicolon)
//         {
//             var semicolon = Expect(TokenKind.Semicolon);
//             return new ModuleDeclarationSyntax(moduleKeyword, identifier, semicolon);
//         }

//         var open = Expect(TokenKind.OpenBrace);
//         var body = new List<SyntaxNode>();

//         while (_iterator.Current.Kind != TokenKind.CloseBrace && !_iterator.IsAtEnd)
//         {
//             body.Add(ParseStatement());
//         }
//     }
// }