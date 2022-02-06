using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.ConventionServiceTargets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceRegistrationOptionsTests {
        [Fact]
        public void AddAssembly_Adds_Assembly() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddAssembly(typeof(NamedService).Assembly));
            Assert.Equal(typeof(NamedService).Assembly, Assert.Single(options.Assemblies));
        }

        [Fact]
        public void AddServiceTypeProvider_Adds_ServiceTypeProvider() {
            ServiceTypeProvider serviceTypeProvider = implementationType => Enumerable.Empty<Type>();
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddServiceTypeProvider(serviceTypeProvider));
            Assert.Equal(serviceTypeProvider, Assert.Single(options.ServiceTypeProviders).ServiceTypeProvider);
            Assert.Null(Assert.Single(options.ServiceTypeProviders).ServiceLifetimeProvider);
        }


        [Fact]
        public void AddServiceTypeProvider_Adds_ServiceTypeProvider_With_ServiceLifetimeProvider() {
            ServiceTypeProvider serviceTypeProvider = implementationType => Enumerable.Empty<Type>();
            ServiceLifetimeProvider serviceLifetimeProvider = (serviceType, implementationType) => ServiceLifetime.Scoped;
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddServiceTypeProvider(serviceTypeProvider, serviceLifetimeProvider));
            Assert.Equal(serviceTypeProvider, Assert.Single(options.ServiceTypeProviders).ServiceTypeProvider);
            Assert.Equal(serviceLifetimeProvider, Assert.Single(options.ServiceTypeProviders).ServiceLifetimeProvider);
        }

        [Fact]
        public void UseDefaultServiceLifetime_Sets_DefaultServiceLifetime() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.UseDefaultServiceLifetime(ServiceLifetime.Singleton));
            Assert.Equal(ServiceLifetime.Singleton, options.DefaultServiceLifetime);
        }

        [Fact]
        public void UseServiceRegistrar_Sets_ServiceRegistrar() {
            ServiceRegistrar serviceRegistrar = (services, serviceType, implementationType, serviceLifetime) => services;
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.UseServiceRegistrar(serviceRegistrar));
            Assert.Equal(serviceRegistrar, options.ServiceRegistrar);
        }
    }
}
