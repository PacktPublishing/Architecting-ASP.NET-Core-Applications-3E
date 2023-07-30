using System.Collections.Immutable;

namespace OperationResult.MultipleErrorsWithValue;

public record class OperationResult
{
    public OperationResult()
    {
        Errors = ImmutableList<string>.Empty;
    }
    public OperationResult(params string[] errors)
    {
        Errors = errors.ToImmutableList();
    }

    public bool Succeeded => !HasErrors();
    public int? Value { get; init; }

    public IReadOnlyCollection<string> Errors { get; init; }
    public bool HasErrors()
    {
        return Errors?.Count > 0;
    }
}
