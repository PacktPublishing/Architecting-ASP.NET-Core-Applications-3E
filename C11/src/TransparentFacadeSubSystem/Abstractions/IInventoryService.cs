namespace TransparentFacadeSubSystem.Abstractions;

public interface IInventoryService
{
    bool CheckStock(string productId, int quantity);
}