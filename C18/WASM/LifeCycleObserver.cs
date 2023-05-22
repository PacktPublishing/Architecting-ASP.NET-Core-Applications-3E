using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace WASM;

public class LifeCycleObserver : ComponentBase
{
    [Parameter]
    public int ClickCount { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        Console.WriteLine("LifeCycleObserver.BuildRenderTree");

        builder.OpenElement(0, "button");
        builder.AddAttribute(1, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => ClickCount++));
        builder.AddContent(2, $"Click me ({ClickCount})");
        builder.CloseElement();
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Console.WriteLine("LifeCycleObserver.SetParametersAsync");
        return base.SetParametersAsync(parameters);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Console.WriteLine($"LifeCycleObserver.OnAfterRender|firstRender: {firstRender}");
        base.OnAfterRender(firstRender);
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        Console.WriteLine($"LifeCycleObserver.OnAfterRenderAsync|firstRender: {firstRender}");
        return base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnInitialized()
    {
        Console.WriteLine("LifeCycleObserver.OnInitialized");
        base.OnInitialized();
    }

    protected override Task OnInitializedAsync()
    {
        Console.WriteLine("LifeCycleObserver.OnInitializedAsync");
        return base.OnInitializedAsync();
    }

    protected override void OnParametersSet()
    {
        Console.WriteLine("LifeCycleObserver.OnParametersSet");
        base.OnParametersSet();
    }

    protected override Task OnParametersSetAsync()
    {
        Console.WriteLine("LifeCycleObserver.OnParametersSetAsync");
        return base.OnParametersSetAsync();
    }

    protected override bool ShouldRender()
    {
        var shouldRender = base.ShouldRender();
        Console.WriteLine($"LifeCycleObserver.ShouldRender: {shouldRender}");
        return shouldRender;
    }
}
