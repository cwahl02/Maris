using Maris.Compiler.Lexer;
using Xunit;

public partial class LexerTests
{
    // TODO: Make string field nullable to make it
    // so you only have to pass in strings for identifiers and literals
    private void Equal(
        string source,
        params (TokenType type, string text)[] expectedTokens)
    {
        var lexer = new Lexer(source);
        var tokens = lexer.Lex();

        var expected = new List<Token>();
        int currentSearchIndex = 0;

        foreach (var (type, text) in expectedTokens)
        {
            // Find the occurrence *after* the last processed token
            int start = source.IndexOf(text, currentSearchIndex);
            
            if (start == -1)
            {
                throw new ArgumentException($"The expected text snippet '{text}' was not found in the source string starting at index {currentSearchIndex}.");
            }

            // Create token using the proper TextSpan constructor 
            // (Adjusted based on your `new TextSpan(...)` syntax in the snippet)
            expected.Add(new Token(type, start, text.Length, source));
            
            // Advance search position past this token to handle duplicates safely
            currentSearchIndex = start + text.Length;
        }

        // xUnit v3 collection assertion will now pass because Token implements IEquatable<Token>
        Assert.Equal(expected, tokens);

        // Optional: Output the tokens for debugging purposes
        _output.WriteLine("Expected Tokens:");
        foreach (var token in expected)
        {
            _output.WriteLine(token.ToString());
        }
    }
}