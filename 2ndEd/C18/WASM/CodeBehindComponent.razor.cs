using Microsoft.AspNetCore.Components;

namespace WASM;

public partial class CodeBehindComponent
{
    [Parameter]
    public string? Text { get; set; }
}
