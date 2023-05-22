using Microsoft.AspNetCore.Components;

namespace WASM;

public class PlainComponent : IComponent
{
    [Parameter]
    public string? Text { get; set; }

    public void Attach(RenderHandle renderHandle)
    {
        renderHandle.Render(builder =>
        {
            builder.OpenElement(0, "h4");
            builder.AddAttribute(1, "class", "hello-world");
            builder.AddContent(2, Text);
            builder.CloseElement();
        });
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue("Text", out string? text))
        {
            Text = text;
        }
        return Task.CompletedTask;
    }
}
