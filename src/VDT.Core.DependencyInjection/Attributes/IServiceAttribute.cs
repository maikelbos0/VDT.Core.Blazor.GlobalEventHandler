using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection.Attributes {
    internal interface IServiceAttribute {
        ServiceLifetime ServiceLifetime { get; }
        Type ImplementationType { get; }
    }
}
