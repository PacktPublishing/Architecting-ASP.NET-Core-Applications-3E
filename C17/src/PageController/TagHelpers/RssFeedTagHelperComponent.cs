using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PageController.TagHelpers;

public class RssFeedTagHelperComponent : TagHelperComponent
{
    private readonly RssFeedTagHelperComponentOptions _options;
    public RssFeedTagHelperComponent(RssFeedTagHelperComponentOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context.TagName == "head")
        {
            output.PostContent.AppendHtml(
                $@"<link href=""{_options.Href}"" type=""{_options.Type}"" rel=""{_options.Rel}"" title=""{_options.Title}"">"
            );
        }
    }
}
