using System.Collections;

namespace REPR.Baskets.Contracts;

public record class FetchItemsResponse(IEnumerable<FetchItemsResponseItem> Items) : IEnumerable<FetchItemsResponseItem>
{
    public IEnumerator<FetchItemsResponseItem> GetEnumerator()
        => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)Items).GetEnumerator();
}

