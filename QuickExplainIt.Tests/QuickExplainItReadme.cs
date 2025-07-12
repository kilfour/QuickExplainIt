namespace QuickExplainIt.Tests;

[Doc(Order = "0", Caption = "QuickExplainIt", Content =
@"`QuickExplainIt` is a lightweight documentation generator for C# projects,
designed to turn `[Doc]`-annotated test classes and methods into clean, structured Markdown files.
It leverages the `QuickPulse` library for declarative flow composition and supports
both single-file and multi-file generation with namespace-based filtering.
")]
public class QuickExplainItReadme
{
    [Fact]
    public void GenerateReadme()
    {
        new Document().ToFiles([new("README.md",
            [ "QuickExplainIt.Tests"
            , "QuickExplainIt.Tests.Documenting"
            , "QuickExplainIt.Tests.Text"
            ])], typeof(QuickExplainItReadme).Assembly);
    }
}
