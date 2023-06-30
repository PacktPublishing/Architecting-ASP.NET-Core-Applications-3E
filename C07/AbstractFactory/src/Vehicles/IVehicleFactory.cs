namespace Vehicles;

public interface IVehicleFactory
{
    ICar CreateCar();
    IBike CreateBike();
}
