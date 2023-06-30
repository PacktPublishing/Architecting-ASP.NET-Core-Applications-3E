using Xunit;

namespace Vehicles;

public abstract class BaseAbstractFactoryTest<TConcreteFactory, TExpectedCar, TExpectedBike>
    where TConcreteFactory : IVehicleFactory, new()
{
    [Fact]
    public void Should_create_a_ICar_of_type_TExpectedCar()
    {
        // Arrange
        IVehicleFactory vehicleFactory = new TConcreteFactory();
        var expectedCarType = typeof(TExpectedCar);

        // Act
        ICar result = vehicleFactory.CreateCar();

        // Assert
        Assert.IsType(expectedCarType, result);
    }

    [Fact]
    public void Should_create_a_IBike_of_type_TExpectedBike()
    {
        // Arrange
        IVehicleFactory vehicleFactory = new TConcreteFactory();
        var expectedBikeType = typeof(TExpectedBike);

        // Act
        IBike result = vehicleFactory.CreateBike();

        // Assert
        Assert.IsType(expectedBikeType, result);
    }
}