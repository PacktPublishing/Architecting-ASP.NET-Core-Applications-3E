using Vehicles.Models;
using Xunit;

namespace Vehicles;

public class LowEndVehicleFactoryTest : BaseAbstractFactoryTest<LowEndVehicleFactory, LowEndCar, LowEndBike> { }
public class MidRangeVehicleFactoryTest : BaseAbstractFactoryTest<MidRangeVehicleFactory, MidRangeCar, MidRangeBike> { }
public class HighEndVehicleFactoryTest : BaseAbstractFactoryTest<HighEndVehicleFactory, HighEndCar, HighEndBike> { }

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