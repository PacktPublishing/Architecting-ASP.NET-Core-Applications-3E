using Vehicles.Models;

namespace Vehicles
{
    public class HighGradeVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new HighGradeBike();
        public ICar CreateCar() => new HighGradeCar();
    }
}
