namespace PageController.TagHelpers;

public class RssFeedTagHelperComponentOptions
{
    public string Href { get; set; } = "/feed.xml";
    public string Type { get; set; } = "application/atom+xml";
    public string Rel { get; set; } = "alternate";
    public string Title { get; set; } = "";
}
