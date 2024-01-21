using TransparentFacadeSubSystem.Abstractions;

namespace Facade;

public class UpdatedInventoryService : IInventoryService
{
    public bool CheckStock(string productId, int quantity)
    {
        return false; // Simplified for example
    }
}
