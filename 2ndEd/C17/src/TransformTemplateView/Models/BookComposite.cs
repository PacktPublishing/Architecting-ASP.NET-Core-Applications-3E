using System.Collections.ObjectModel;

namespace TransformTemplateView.Models;

public abstract class BookComposite : IComponent
{
    protected readonly List<IComponent> children;
    public BookComposite(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        children = new List<IComponent>();
    }

    public string Name { get; }
    public virtual ReadOnlyCollection<IComponent> Components => new ReadOnlyCollection<IComponent>(children);

    public virtual string Type => GetType().Name;
    public virtual void Add(IComponent bookComponent) => children.Add(bookComponent);
    public virtual int Count() => children.Sum(child => child.Count());
    public virtual void Remove(IComponent bookComponent) => children.Remove(bookComponent);
    public virtual void AddRange(IComponent[] components) => children.AddRange(components);
}
