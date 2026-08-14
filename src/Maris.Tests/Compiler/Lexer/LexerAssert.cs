using Maris.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
public static class LexerAssert
{
    public static void DoesNotContainTokenTypeInvalid(IEnumerable<Token> tokens)
    {
        foreach (var token in tokens)
        {
            if (token.Type == TokenType.Invalid)
            {
                throw new Exception($"Token '{token.Value}' is invalid.");
            }
        }
    }

    public static void ContainsText(IEnumerable<Token> tokens, params string[] texts)
    {
        HashSet<string> tokenValues = [.. tokens.Select(t => t.Value.ToString())];
        foreach (var text in texts)
        {
            if (!tokenValues.Contains(text))
            {
                throw new Exception($"Token '{text}' not found.");
            }
        }
    }

    public static void ContainsTokenTypes(IEnumerable<Token> tokens, params TokenType[] types)
    {
        HashSet<TokenType> tokenTypes = [.. tokens.Select(t => t.Type)];
        foreach (var type in types)
        {
            if (!tokenTypes.Contains(type))
            {
                throw new Exception($"Token of type '{type}' not found.");
            }
        }
    }
}