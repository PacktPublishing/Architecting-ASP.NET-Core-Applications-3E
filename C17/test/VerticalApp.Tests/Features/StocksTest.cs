using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;
using System.Text.Json;
using VerticalApp.Data;
using VerticalApp.Models;
using Xunit;

namespace VerticalApp.Features.Stocks;

public class StocksTest
{
    public class EFCoreErrors
    {
        [Fact]
        public async Task Erroneous_testing_methodology()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            using var sharedScope = application.Services.CreateScope();
            var db = sharedScope.ServiceProvider.GetRequiredService<ProductContext>();
            var product = db.Products.First();
            var productId = product.Id;

            // Act
            product.Name = "Never Persisted";

            // Assert (error)
            var p = db.Products.Find(productId);
            Assert.NotNull(p);
            Assert.Equal("Never Persisted", p.Name);
        }

        [Fact]
        [Trait("FailureExpected", "true")]
        public async Task Erroneous_testing_methodology_fixed()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            using var sharedScope = application.Services.CreateScope();
            var db = sharedScope.ServiceProvider.GetRequiredService<ProductContext>();
            var product = db.Products.First();
            var productId = product.Id;

            // Act
            product.Name = "Never Persisted";

            // Assert (correct)
            using var assertScope = application.Services.CreateScope();
            var assertDb = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
            var p2 = assertDb.Products.Find(productId);
            Assert.NotNull(p2);
            Assert.Equal("Never Persisted", p2.Name);
        }
    }



    private static async Task SeederDelegate(ProductContext db)
    {
        db.Products.RemoveRange(db.Products.ToArray());
        await db.Products.AddAsync(new Product(
            id: 4,
            name: "Ghost Pepper",
            quantityInStock: 10
        ));
        await db.Products.AddAsync(new Product(
            id: 5,
            name: "Carolina Reaper",
            quantityInStock: 10
        ));
        await db.SaveChangesAsync();
    }

    public class AddStocksTest : StocksTest
    {
        [Fact]
        public async Task Should_increment_QuantityInStock_by_the_specified_amount()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.Send(new AddStocks.Command
            {
                ProductId = 4,
                Amount = 10
            });

            // Assert
            using var assertScope = application.Services.CreateScope();
            var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
            var peppers = await db.Products.FindAsync(4);
            Assert.NotNull(peppers);
            Assert.Equal(20, peppers!.QuantityInStock);
        }

        [Fact]
        public async Task Should_throw_a_ProductNotFoundException_when_no_product_is_found_for_the_specified_ProductId()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => mediator.Send(new AddStocks.Command
            {
                ProductId = 6,
                Amount = 1
            }));
        }
    }

    public class RemoveStocksTest : StocksTest
    {
        [Fact]
        public async Task Should_decrement_QuantityInStock_by_the_specified_amount()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.Send(new RemoveStocks.Command
            {
                ProductId = 5,
                Amount = 10
            });

            // Assert
            using var assertScope = application.Services.CreateScope();
            var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
            var peppers = await db.Products.FindAsync(5);
            Assert.NotNull(peppers);
            Assert.Equal(0, peppers!.QuantityInStock);
        }

        [Fact]
        public async Task Should_throw_a_NotEnoughStockException_when_the_resulting_QuantityInStock_would_be_less_than_zero()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act & Assert
            await Assert.ThrowsAsync<NotEnoughStockException>(() => mediator.Send(new RemoveStocks.Command
            {
                ProductId = 5,
                Amount = 11
            }));
        }

        [Fact]
        public async Task Should_throw_a_ProductNotFoundException_when_no_product_is_found_for_the_specified_ProductId()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => mediator.Send(new RemoveStocks.Command
            {
                ProductId = 6,
                Amount = 1
            }));
        }
    }

    public class StocksControllerTest : StocksTest
    {
        public class AddAsync : StocksControllerTest
        {
            [Fact]
            public async Task Should_send_a_valid_AddStocks_Command_to_the_mediator()
            {
                // Arrange
                var mediatorMock = new Mock<IMediator>();
                AddStocks.Command? addStocksCommand = default;
                mediatorMock
                    .Setup(x => x.Send(It.IsAny<AddStocks.Command>(), It.IsAny<CancellationToken>()))
                    .Callback((IRequest<AddStocks.Result> request, CancellationToken cancellationToken) => addStocksCommand = request as AddStocks.Command)
                ;
                await using var application = new VerticalAppApplication(
                    afterConfigureServices: services => services
                        .AddSingleton(mediatorMock.Object)
                );
                var client = application.CreateClient();
                var httpContent = JsonContent.Create(
                    new { amount = 1 },
                    options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
                );

                // Act
                var response = await client.PostAsync("/products/5/add-stocks", httpContent);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(addStocksCommand);
                response.EnsureSuccessStatusCode();
                mediatorMock.Verify(
                    x => x.Send(It.IsAny<AddStocks.Command>(), It.IsAny<CancellationToken>()),
                    Times.Once()
                );
                Assert.Equal(5, addStocksCommand!.ProductId);
                Assert.Equal(1, addStocksCommand!.Amount);
            }
        }
        public class RemoveAsync : StocksControllerTest
        {
            [Fact]
            public async Task Should_send_a_valid_RemoveStocks_Command_to_the_mediator()
            {
                // Arrange
                var mediatorMock = new Mock<IMediator>();
                RemoveStocks.Command? removeStocksCommand = default;
                mediatorMock
                    .Setup(x => x.Send(It.IsAny<RemoveStocks.Command>(), It.IsAny<CancellationToken>()))
                    .Callback((IRequest<RemoveStocks.Result> request, CancellationToken cancellationToken) => removeStocksCommand = request as RemoveStocks.Command)
                ;
                await using var application = new VerticalAppApplication(
                    afterConfigureServices: services => services
                        .AddSingleton(mediatorMock.Object)
                );
                var client = application.CreateClient();
                var httpContent = JsonContent.Create(
                    new { amount = 1 },
                    options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
                );

                // Act
                var response = await client.PostAsync("/products/5/remove-stocks", httpContent);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(removeStocksCommand);
                response.EnsureSuccessStatusCode();
                mediatorMock.Verify(
                    x => x.Send(It.IsAny<RemoveStocks.Command>(), It.IsAny<CancellationToken>()),
                    Times.Once()
                );
                Assert.Equal(5, removeStocksCommand!.ProductId);
                Assert.Equal(1, removeStocksCommand!.Amount);
            }
        }
    }
}
