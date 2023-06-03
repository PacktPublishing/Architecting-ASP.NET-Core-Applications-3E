using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using TransformTemplateView.Models;
using TransformTemplateView.Services;

namespace TransformTemplateView.Pages.BookStore;

public class IndexModel : PageModel
{
    private readonly ICorporationFactory _corporationFactory;

    public IndexModel(ICorporationFactory corporationFactory)
    {
        _corporationFactory = corporationFactory ?? throw new ArgumentNullException(nameof(corporationFactory));
    }

    public ReadOnlyCollection<IComponent> Components { get; private set; } = new(Array.Empty<IComponent>());

    public void OnGet()
    {
        var corporation = _corporationFactory.Create();
        Components = new ReadOnlyCollection<IComponent>(new IComponent[] { corporation });
    }
}
