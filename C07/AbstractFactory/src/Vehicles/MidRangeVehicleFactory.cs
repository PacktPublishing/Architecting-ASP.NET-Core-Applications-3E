using Vehicles.Models;

namespace Vehicles;

public class MidRangeVehicleFactory : IVehicleFactory
{
    public IBike CreateBike() => new MidRangeBike();
    public ICar CreateCar() => new MidRangeCar();
}
