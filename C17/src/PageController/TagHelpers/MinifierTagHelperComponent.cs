using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PageController.TagHelpers;

public class MinifierTagHelperComponent : TagHelperComponent
{
    public override int Order => int.MaxValue;

    public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();
        var content = childContent.GetContent();
        var result = Minify(content);
        output.Content.SetHtmlContent(result);
    }

    private static string Minify(string input)
    {
        var lines = input.Split(Environment.NewLine);
        using var sw = new StringWriter();
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmedLine))
            {
                continue;
            }
            if (AddStartSpace(trimmedLine))
            {
                sw.Write(" ");
            }
            sw.Write(trimmedLine);
            if(AddEndSpace(trimmedLine))
            {
                sw.Write(" ");
            }
        }
        return sw.ToString();
    }

    private static bool AddStartSpace(string line)
    {
        return !line.StartsWith("<");
    }

    private static bool AddEndSpace(string line)
    {
        return !line.EndsWith(">");
    }
}
