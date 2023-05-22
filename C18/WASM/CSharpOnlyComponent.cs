using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace WASM;

public class CSharpOnlyComponent : ComponentBase
{
    [Parameter]
    public string? Text { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "h1");
        builder.AddAttribute(1, "class", "hello-world");
        builder.AddContent(2, Text);
        builder.CloseElement();
    }
}
