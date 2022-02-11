using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionExtensionsTests {
        [Fact]
        public void AddServices_Uses_ServiceLifetimeProvider() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))) {
                    ServiceLifetimeProvider = (serviceType, implementationType) => ServiceLifetime.Singleton
                });
                options.DefaultServiceLifetime = ServiceLifetime.Scoped;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_DefaultServiceLifetime_If_No_ServiceLifetime_Found() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))) {
                    ServiceLifetimeProvider = (serviceType, implementationType) => null
                });
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_DefaultServiceLifetime_If_No_ServiceLifetimeProvider_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_ServiceRegistrar_If_Provided() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
                options.ServiceRegistrar = (services, serviceType, implementationType, serviceLifetime) => services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped));
            });

            Assert.Equal(ServiceLifetime.Scoped, Assert.Single(services, s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Creates_ServiceRegistrations_If_No_ServiceRegistrar_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
                options.DefaultServiceLifetime = ServiceLifetime.Scoped;
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(INamedService));

            Assert.Equal(typeof(NamedService), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Scoped, service.Lifetime);
        }

        [Fact]
        public void AddServices_Adds_Services_From_All_ServiceTypeProviders() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i == typeof(IGenericInterface))));
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
            });

            Assert.Contains(services, s => s.ServiceType == typeof(INamedService));
            Assert.Contains(services, s => s.ServiceType == typeof(IGenericInterface));
        }

        [Fact]
        public void AddServices_Adds_No_Services_When_No_Assemblies_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.ServiceTypeProviders.Add(new ServiceTypeProviderOptions(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface))));
            });

            Assert.Empty(services);
        }

        [Fact]
        public void AddServices_Adds_No_Services_When_No_ServiceTypeProviders_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
            });

            Assert.Empty(services);
        }
    }
}
