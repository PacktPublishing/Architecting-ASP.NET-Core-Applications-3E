using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Vehicles
{
    public abstract class AbstractFactoryBaseTestData : IEnumerable<object[]>
    {
        private readonly TheoryData<IVehicleFactory, Type> _data = new TheoryData<IVehicleFactory, Type>();

        protected void AddTestData<TConcreteFactory, TExpectedVehicle>()
            where TConcreteFactory : IVehicleFactory, new()
        {
            _data.Add(new TConcreteFactory(), typeof(TExpectedVehicle));
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
