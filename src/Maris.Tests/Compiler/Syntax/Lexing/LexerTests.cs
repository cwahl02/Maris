using Maris.Compiler.Syntax.Lexing;
using Maris.Core.Text;

namespace Maris.Tests.Compiler.Syntax.Lexing;

public class LexerTests : IDisposable
{
    private IReadOnlyList<SyntaxToken> _tokens;
    private ITestOutputHelper _output;
    public LexerTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    public void Dispose()
    {
        foreach (var token in _tokens)
        {
            _output.WriteLine(token.ToString());
        }
    }
}