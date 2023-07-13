using Moq;
using Strategy.Controllers;
using Strategy.Data;
using Strategy.Models;
using Strategy.Services;

namespace Strategy;
public class InjectAbstractionTest
{
    public static Location ExpectedLocation { get; } = new Location(11, "Montréal", "CA");

    [Fact]
    public async Task Mock_the_IDatabase()
    {
        // Arrange
        var databaseMock = new Mock<IDatabase>();
        databaseMock.Setup(x => x.ReadManyAsync<Location>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Location[] { ExpectedLocation })
        ;
        var sqlLocationService = new SqlLocationService(databaseMock.Object);
        var sqlController = new InjectAbstractionLocationsController(sqlLocationService);

        // Act
        var result = await sqlController.GetAsync(CancellationToken.None);

        // Assert the controller returned the expected value
        Assert.Collection(result,
            location =>
            {
                Assert.Equal(ExpectedLocation.Id, location.Id);
                Assert.Equal(ExpectedLocation.Name, location.Name);
            }
        );

        // (optional) Assert the IDatabase.ReadManyAsync was called once by the SqlLocationService.
        databaseMock.Verify(x => x
            .ReadManyAsync<Location>(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once()
        );
    }

    [Fact]
    public async Task Use_the_InMemoryLocationService()
    {
        // Arrange
        var inMemoryLocationService = new InMemoryLocationService();
        var devController = new InjectAbstractionLocationsController(inMemoryLocationService);
        var expectedLocation = await inMemoryLocationService.FetchAllAsync(CancellationToken.None);

        // Act
        var result = await devController.GetAsync(CancellationToken.None);

        // Assert the controller returned the correct values (this is hacky)
        var expectedArray = expectedLocation.ToArray();
        var resultArray = result.ToArray();
        for (var i = 0; i < expectedArray.Length; i++)
        {
            Assert.Equal(expectedArray[i].Id, resultArray[i].Id);
            Assert.Equal(expectedArray[i].Name, resultArray[i].Name);
        }
    }

    [Fact]
    public async Task Mock_the_ILocationService()
    {
        // Arrange
        var locationServiceMock = new Mock<ILocationService>();
        locationServiceMock.Setup(x => x.FetchAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Location[] { ExpectedLocation })
        ;
        var testController = new InjectAbstractionLocationsController(locationServiceMock.Object);

        // Act
        var result = await testController.GetAsync(CancellationToken.None);

        // Assert the controller returned the expected value
        Assert.Collection(result,
            location =>
            {
                Assert.Equal(ExpectedLocation.Id, location.Id);
                Assert.Equal(ExpectedLocation.Name, location.Name);
            }
        );

        // (optional) Assert the ILocationService.FetchAllAsync method was called once
        locationServiceMock.Verify(x => x
            .FetchAllAsync(It.IsAny<CancellationToken>()),
            Times.Once()
        );
    }
}
