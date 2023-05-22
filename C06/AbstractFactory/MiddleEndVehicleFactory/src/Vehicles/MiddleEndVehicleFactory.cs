using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models;

namespace Vehicles
{
    public class MiddleEndVehicleFactory : IVehicleFactory
    {
        public IBike CreateBike() => new MiddleGradeBike();
        public ICar CreateCar() => new MiddleGradeCar();
    }
}
