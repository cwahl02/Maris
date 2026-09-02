using Maris.Core.Text;

namespace Maris.Tests.Compiler.Text;

public class TextWindowTests
{
    [Fact]
    public void Peek_NegativeOffsetBeforeStartOfText_ReturnsNulChar()
    {
        var window = new TextWindow("abc");

        Assert.Equal('\0', window.Peek(-1));
    }

    [Fact]
    public void Peek_NegativeOffsetWithinText_ReturnsPreviousCharacter()
    {
        var window = new TextWindow("abc");
        window.Advance(2);

        Assert.Equal('b', window.Peek(-1));
    }

    [Fact]
    public void Peek_OffsetPastEndOfText_ReturnsNulChar()
    {
        var window = new TextWindow("abc");

        Assert.Equal('\0', window.Peek(10));
    }
}
