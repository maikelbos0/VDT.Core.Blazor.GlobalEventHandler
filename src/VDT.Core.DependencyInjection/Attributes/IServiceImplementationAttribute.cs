using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection.Attributes {
    internal interface IServiceImplementationAttribute {
        ServiceLifetime ServiceLifetime { get; }
        Type ServiceType { get; }
    }
}
