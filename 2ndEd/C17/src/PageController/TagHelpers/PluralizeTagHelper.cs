using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PageController.TagHelpers;

[HtmlTargetElement("pluralize", TagStructure = TagStructure.WithoutEndTag)]
public class PluralizeTagHelper : TagHelper
{
    public int Count { get; set; }
    public string? Singular { get; set; }
    public string? Plural { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var text = Count > 1 ? Plural : Singular;
        if (text is not null)
        {
            text = string.Format(text, Count);
        }
        output.TagName = null;
        output.Content.SetContent(text);
    }
}
