using TransparentFacadeSubSystem.Abstractions;

namespace TransparentFacadeSubSystem;

// Subsystem: Inventory
public class InventoryService : IInventoryService
{
    public bool CheckStock(string productId, int quantity)
    {
        // Check if the product is available in the desired quantity
        return true; // Simplified for example
    }
}
