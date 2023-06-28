using Vehicles.Models;

namespace Vehicles;

public class MiddleEndVehicleFactory : IVehicleFactory
{
    public IBike CreateBike() => new MiddleEndBike();
    public ICar CreateCar() => new MiddleEndCar();
}
