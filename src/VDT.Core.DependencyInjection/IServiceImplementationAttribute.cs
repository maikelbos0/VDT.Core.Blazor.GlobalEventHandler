using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    internal interface IServiceImplementationAttribute {
        ServiceLifetime ServiceLifetime { get; }
        Type ServiceType { get; }
    }
}
