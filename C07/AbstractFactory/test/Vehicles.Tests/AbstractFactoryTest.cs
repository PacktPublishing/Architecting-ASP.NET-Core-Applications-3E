using Vehicles.Models;
using Xunit;

namespace Vehicles;

public class LowGradeVehicleFactoryTest : BaseAbstractFactoryTest<LowGradeVehicleFactory, LowGradeCar, LowGradeBike> { }
public class MiddleEndVehicleFactoryTest : BaseAbstractFactoryTest<MiddleEndVehicleFactory, MiddleGradeCar, MiddleGradeBike> { }
public class HighGradeVehicleFactoryTest : BaseAbstractFactoryTest<HighGradeVehicleFactory, HighGradeCar, HighGradeBike> { }

public abstract class BaseAbstractFactoryTest<TConcreteFactory, TExpectedCar, TExpectedBike>
    where TConcreteFactory : IVehicleFactory, new()
{
    [Fact]
    public void Should_create_a_Car_of_the_specified_type()
    {
        // Arrange
        var vehicleFactory = new TConcreteFactory();
        var expectedCarType = typeof(TExpectedCar);

        // Act
        ICar result = vehicleFactory.CreateCar();

        // Assert
        Assert.IsType(expectedCarType, result);
    }

    [Fact]
    public void Should_create_a_Bike_of_the_specified_type()
    {
        // Arrange
        var vehicleFactory = new TConcreteFactory();
        var expectedBikeType = typeof(TExpectedBike);

        // Act
        IBike result = vehicleFactory.CreateBike();

        // Assert
        Assert.IsType(expectedBikeType, result);
    }
}