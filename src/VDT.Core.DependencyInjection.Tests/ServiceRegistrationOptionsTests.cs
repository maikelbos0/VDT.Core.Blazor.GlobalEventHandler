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
        public void AddServiceTypeFinder_Adds_ServiceTypeFinder() {
            ServiceTypeFinder serviceTypeFinder = implementationType => Enumerable.Empty<Type>();
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddServiceTypeFinder(serviceTypeFinder));
            Assert.Equal(serviceTypeFinder, Assert.Single(options.ServiceTypeFinders).ServiceTypeFinder);
            Assert.Null(Assert.Single(options.ServiceTypeFinders).ServiceLifetimeProvider);
        }


        [Fact]
        public void AddServiceTypeFinder_Adds_ServiceTypeFinder_With_ServiceLifetimeProvider() {
            ServiceTypeFinder serviceTypeFinder = implementationType => Enumerable.Empty<Type>();
            ServiceLifetimeProvider serviceLifetimeProvider = (serviceType, implementationType) => ServiceLifetime.Scoped;
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddServiceTypeFinder(serviceTypeFinder, serviceLifetimeProvider));
            Assert.Equal(serviceTypeFinder, Assert.Single(options.ServiceTypeFinders).ServiceTypeFinder);
            Assert.Equal(serviceLifetimeProvider, Assert.Single(options.ServiceTypeFinders).ServiceLifetimeProvider);
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
