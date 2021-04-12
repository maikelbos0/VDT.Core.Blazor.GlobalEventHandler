using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Options to set up decorators to a service
    /// </summary>
    public sealed class DecoratorOptions {
        private static readonly MethodInfo addDecoratorMethod = typeof(DecoratorOptions).GetMethod(nameof(AddDecorator), 1, BindingFlags.Public | BindingFlags.Instance, typeof(MethodInfo));

        private readonly Type serviceType;
        private readonly Type implementationType;

        internal List<DecoratorPolicy> Policies { get; } = new List<DecoratorPolicy>();

        internal DecoratorOptions(Type serviceType, Type implementationType) {
            this.serviceType = serviceType;
            this.implementationType = implementationType;
        }

        /// <summary>
        /// Add a decorator for all methods of the services being registered
        /// </summary>
        /// <typeparam name="TDecorator">Type of the decorator to add to the services being registered</typeparam>
        public void AddDecorator<TDecorator>() where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => true);
        }

        /// <summary>
        /// Add a decorator for <paramref name="method"/>
        /// </summary>
        /// <typeparam name="TDecorator">Type of the decorator to add to the services being registered</typeparam>
        /// <param name="method">The method that should be decorated by <typeparamref name="TDecorator"/></param>
        public void AddDecorator<TDecorator>(MethodInfo method) where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => m == method);
        }

        /// <summary>
        /// Add a decorator for all methods of the services being registered that match <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="TDecorator">Type of the decorator to add to the services being registered</typeparam>
        /// <param name="predicate">The predicate that methods are tested against to see if <typeparamref name="TDecorator"/> should be used</param>
        public void AddDecorator<TDecorator>(Predicate<MethodInfo> predicate) where TDecorator : class, IDecorator {
            Policies.Add(new DecoratorPolicy<TDecorator>(predicate));
        }

        /// <summary>
        /// Adds decorators based on implementations of the <see cref="IDecorateAttribute{TDecorator}"/> interface
        /// </summary>
        public void AddAttributeDecorators() {
            var bindings = new List<DecoratorBinding>();

            bindings.AddRange(GetDecorators(serviceType));
            bindings.AddRange(GetDecorators(implementationType));

            foreach (var binding in bindings) {
                var serviceMethod = binding.GetServiceMethod();

                if (serviceMethod != null) {
                    addDecoratorMethod.MakeGenericMethod(binding.DecoratorType).Invoke(this, new object[] { serviceMethod });
                }
            }
        }

        private IEnumerable<DecoratorBinding> GetDecorators(Type type) {
            return type
                .GetMethods()
                .SelectMany(m => m.GetCustomAttributes().Select(a => new {
                    Method = m,
                    Interface = a.GetType().GetInterfaces().SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDecorateAttribute<>))
                }))
                .Where(d => d.Interface != null)
                .Select(d => new DecoratorBinding(d.Method, serviceType, d.Interface!.GetGenericArguments().First()));
        }
    }
}
