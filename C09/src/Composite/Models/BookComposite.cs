using System.Text;

namespace Composite.Models;

public abstract class BookComposite : IComponent
{
    protected readonly List<IComponent> children = new();
    public BookComposite(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }
    public virtual string Type => GetType().Name;

    public virtual void Add(IComponent bookComponent)
    {
        children.Add(bookComponent);
    }

    public virtual int Count()
    {
        return children.Sum(child => child.Count());
    }

    public virtual string Display()
    {
        var sb = new StringBuilder();
        sb.Append("<section class=\"card\">");
        AppendHeader(sb);
        AppendBody(sb);
        AppendFooter(sb);
        sb.Append("</section>");
        return sb.ToString();
    }

    private void AppendHeader(StringBuilder sb)
    {
        sb.Append($"<{HeadingTagName} class=\"card-header\">");
        sb.Append(Name);
        sb.Append($"<span class=\"badge badge-secondary float-right\">{Count()} books</span>");
        sb.Append($"</{HeadingTagName}>");
    }

    private void AppendBody(StringBuilder sb)
    {
        sb.Append($"<ul class=\"list-group list-group-flush\">");
        children.ForEach(child =>
        {
            sb.Append($"<li class=\"list-group-item\">");
            sb.Append(child.Display());
            sb.Append("</li>");
        });
        sb.Append("</ul>");
    }

    private void AppendFooter(StringBuilder sb)
    {
        sb.Append("<div class=\"card-footer text-muted\">");
        sb.Append($"<small class=\"text-muted text-right\">{Type}</small>");
        sb.Append("</div>");
    }

    public virtual void Remove(IComponent bookComponent)
    {
        children.Remove(bookComponent);
    }

    protected abstract string HeadingTagName { get; }
}
