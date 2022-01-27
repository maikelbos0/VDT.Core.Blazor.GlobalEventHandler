using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;

namespace VDT.Core.DependencyInjection.Tests {
    public abstract class ServiceAttributeTests {
        protected readonly ServiceCollection services;
        protected readonly TestDecorator decorator;

        public ServiceAttributeTests() {
            services = new ServiceCollection();
            decorator = new TestDecorator();

            services.AddSingleton(decorator);
        }
    }
}
