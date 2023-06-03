using System;
using System.Linq;
using Vehicles.Models;
using Xunit;

namespace Vehicles
{
    // Arrange
    public class AbstractFactoryTestCars : AbstractFactoryBaseTestData
    {
        public AbstractFactoryTestCars()
        {
            AddTestData<LowGradeVehicleFactory, LowGradeCar>();
            AddTestData<HighGradeVehicleFactory, HighGradeCar>();
        }
    }

    public class AbstractFactoryTestBikes : AbstractFactoryBaseTestData
    {
        public AbstractFactoryTestBikes()
        {
            AddTestData<LowGradeVehicleFactory, LowGradeBike>();
            AddTestData<HighGradeVehicleFactory, HighGradeBike>();
        }
    }

    // Tests
    public class AbstractFactoryTest
    {
        [Theory]
        [ClassData(typeof(AbstractFactoryTestCars))]
        public void Should_create_a_Car_of_the_specified_type(IVehicleFactory vehicleFactory, Type expectedCarType)
        {
            // Act
            ICar result = vehicleFactory.CreateCar();

            // Assert
            Assert.IsType(expectedCarType, result);
        }

        [Theory]
        [ClassData(typeof(AbstractFactoryTestBikes))]
        public void Should_create_a_Bike_of_the_specified_type(IVehicleFactory vehicleFactory, Type expectedBikeType)
        {
            // Act
            IBike result = vehicleFactory.CreateBike();

            // Assert
            Assert.IsType(expectedBikeType, result);
        }
    }
}
