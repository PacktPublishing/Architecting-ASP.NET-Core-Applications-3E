namespace Vehicles.MidRange;

public class MidRangeVehicleFactory : IVehicleFactory
{
    public IBike CreateBike() => new MidRangeBike();
    public ICar CreateCar() => new MidRangeCar();
}
