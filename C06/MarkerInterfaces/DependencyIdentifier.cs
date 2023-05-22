using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace MarkerInterfaces
{
    public class DependencyIdentifier
    {
        public class CodeSmell : DependencyIdentifier
        {
            [Fact]
            public void ConsumerTest()
            {
                // Arrange
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IStrategyA, StrategyA>()
                    .AddSingleton<IStrategyB, StrategyB>()
                    .AddSingleton<Consumer>()
                    .BuildServiceProvider();

                // Act
                var consumer = serviceProvider.GetService<Consumer>();

                // Assert
                Assert.IsType<StrategyA>(consumer.StrategyA);
                Assert.IsType<StrategyB>(consumer.StrategyB);
            }

            public interface IStrategyA : IStrategy { }
            public interface IStrategyB : IStrategy { }

            public class StrategyA : IStrategyA
            {
                public string Execute() => "StrategyA";
            }

            public class StrategyB : IStrategyB
            {
                public string Execute() => "StrategyB";
            }

            public class Consumer
            {
                public IStrategyA StrategyA { get; }
                public IStrategyB StrategyB { get; }

                public Consumer(IStrategyA strategyA, IStrategyB strategyB)
                {
                    StrategyA = strategyA ?? throw new ArgumentNullException(nameof(strategyA));
                    StrategyB = strategyB ?? throw new ArgumentNullException(nameof(strategyB));
                }
            }
        }

        public class FixedUsage : DependencyIdentifier
        {
            [Fact]
            public void ConsumerTestCase1()
            {
                // Arrange
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<StrategyA>()
                    .AddSingleton<StrategyB>()
                    .AddSingleton(serviceProvider =>
                    {
                        var strategyA = serviceProvider.GetService<StrategyA>();
                        var strategyB = serviceProvider.GetService<StrategyB>();
                        return new Consumer(strategyA, strategyB);
                    })
                    .BuildServiceProvider();

                // Act
                var consumer = serviceProvider.GetService<Consumer>();

                // Assert
                Assert.IsType<StrategyA>(consumer.StrategyA);
                Assert.IsType<StrategyB>(consumer.StrategyB);
            }

            [Fact]
            public void ConsumerTestCase2()
            {
                // Arrange
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<StrategyA>()
                    .AddSingleton<StrategyB>()
                    .AddSingleton(serviceProvider =>
                    {
                        var strategyA = serviceProvider.GetService<StrategyA>();
                        var strategyB = serviceProvider.GetService<StrategyB>();
                        return new Consumer(strategyB, strategyA);
                    })
                    .BuildServiceProvider();

                // Act
                var consumer = serviceProvider.GetService<Consumer>();

                // Assert
                Assert.IsType<StrategyB>(consumer.StrategyA);
                Assert.IsType<StrategyA>(consumer.StrategyB);
            }

            public class StrategyA : IStrategy
            {
                public string Execute() => "StrategyA";
            }

            public class StrategyB : IStrategy
            {
                public string Execute() => "StrategyB";
            }

            public class Consumer
            {
                public IStrategy StrategyA { get; }
                public IStrategy StrategyB { get; }

                public Consumer(IStrategy strategyA, IStrategy strategyB)
                {
                    StrategyA = strategyA ?? throw new ArgumentNullException(nameof(strategyA));
                    StrategyB = strategyB ?? throw new ArgumentNullException(nameof(strategyB));
                }
            }
        }

        public interface IStrategy
        {
            string Execute();
        }
    }
}
