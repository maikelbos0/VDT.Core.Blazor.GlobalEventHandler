﻿using VDT.Core.DependencyInjection.Tests.Decorators;

namespace VDT.Core.DependencyInjection.Tests {
    public interface IScopedServiceImplementationTarget {
        [TestDecorator]
        public string GetValue();
    }
}