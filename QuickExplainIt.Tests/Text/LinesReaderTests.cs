using QuickExplainIt.Text;

namespace QuickExplainIt.Tests.Text;

[Doc(Order = "0-2-1", Caption = "LinesReader: Sequential Line Navigation", Content =
@"`LinesReader` is a small utility used in tests to consume string content line-by-line.
```csharp
var reader = LinesReader.FromText(vcardString);
Assert.Equal(""BEGIN:VCARD"", reader.NextLine());
reader.Skip();
Assert.True(reader.EndOfContent());

Use `FromText(...)` for newline-separated text or `FromStringList(...)` for an array of lines. 
It throws exceptions when reading past the end or using it uninitialized.
```")]
public class LinesReaderTests
{
    [Fact]
    public void NextLine_ReadsLinesInOrder()
    {
        var reader = LinesReader.FromText("line1\r\nline2\r\nline3");
        Assert.Equal("line1", reader.NextLine());
        Assert.Equal("line2", reader.NextLine());
        Assert.Equal("line3", reader.NextLine());
    }

    [Fact]
    public void EndOfContent_ReturnsTrueAtEnd()
    {
        var reader = LinesReader.FromText("onlyline");
        reader.NextLine();
        Assert.True(reader.EndOfContent());
    }

    [Fact]
    public void EndOfContent_ReturnsFalseBeforeEnd()
    {
        var reader = LinesReader.FromText("a\r\nb");
        reader.NextLine();
        Assert.False(reader.EndOfContent());
    }

    [Fact]
    public void Skip_SkipsLine()
    {
        var reader = LinesReader.FromText("first\r\nskipme\r\nthird");
        reader.NextLine();
        reader.Skip();
        Assert.Equal("third", reader.NextLine());
    }

    [Fact]
    public void SkipMany_SkipsCorrectNumberOfLines()
    {
        var reader = LinesReader.FromText("a\r\nb\r\nc\r\nd");
        reader.Skip(2);
        Assert.Equal("c", reader.NextLine());
    }

    [Fact]
    public void FromText_EmptyString_ReturnsEmptyLine()
    {
        var reader = LinesReader.FromText("");
        Assert.Equal("", reader.NextLine());
        Assert.True(reader.EndOfContent());
    }

    [Fact]
    public void NextLine_Throws_WhenReadingPastEnd()
    {
        var reader = LinesReader.FromText("only");
        reader.NextLine();
        Assert.Throws<InvalidOperationException>(reader.NextLine);
    }

    [Fact]
    public void FromStringList_Works()
    {
        var reader = LinesReader.FromStringList(["one", "two"]);
        Assert.Equal("one", reader.NextLine());
        Assert.Equal("two", reader.NextLine());
    }
}
