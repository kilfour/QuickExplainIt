namespace QuickExplainIt.Text;

public class LinesReader
{
    private readonly string[] lines;
    private int currentIndex = -1;

    private LinesReader(string[] lines)
    {
        this.lines = lines;
        if (lines.Length > 0)
            currentIndex = 0;
    }

    public static LinesReader FromText(string text) =>
        new(text.Split(Environment.NewLine));

    public static LinesReader FromStringList(string[] lines) =>
        new(lines);

    public string NextLine()
    {
        if (currentIndex == -1)
            throw new InvalidOperationException("No text was provided to the reader.");

        if (currentIndex >= lines.Length)
            throw new InvalidOperationException("Attempted to read past the end of content.");

        return lines[currentIndex++];
    }

    public void Skip() => currentIndex++;
    public void Skip(int linesToSkip) => currentIndex += linesToSkip;
    public bool EndOfContent() => currentIndex >= lines.Length;
}
