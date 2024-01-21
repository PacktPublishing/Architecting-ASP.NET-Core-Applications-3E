namespace TransparentFacadeSubSystem.Abstractions;

public interface IShippingService
{
    void ScheduleShipping(int orderId);
}