using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Options to set up decorators to a service
    /// </summary>
    /// <typeparam name="TService">The type of the service to add decorators to</typeparam>
    public sealed class DecoratorOptions<TService> where TService : class {
        private static readonly MethodInfo addDecoratorMethod = typeof(DecoratorOptions<TService>).GetMethod(nameof(AddDecorator), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(MethodInfo) }, null) ?? throw new InvalidOperationException($"Method '{nameof(DecoratorOptions<TService>)}.{nameof(AddDecorator)}' was not found.");

        internal List<DecoratorPolicy> Policies { get; } = new List<DecoratorPolicy>();        

        /// <summary>
        /// Add a decorator for all methods of <typeparamref name="TService"/>
        /// </summary>
        /// <typeparam name="TDecorator">Type of the decorator to add to <typeparamref name="TService"/></typeparam>
        public void AddDecorator<TDecorator>() where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => true);
        }

        /// <summary>
        /// Add a decorator for <paramref name="method"/> of <typeparamref name="TService"/>
        /// </summary>
        /// <typeparam name="TDecorator">Type of the decorator to add to <typeparamref name="TService"/></typeparam>
        /// <param name="method">The method of <typeparamref name="TService"/> that should use <typeparamref name="TDecorator"/></param>
        public void AddDecorator<TDecorator>(MethodInfo method) where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => m == method);
        }

        /// <summary>
        /// Add a decorator for all methods of <typeparamref name="TService"/> that match <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="TDecorator">Type of the decorator to add to <typeparamref name="TService"/></typeparam>
        /// <param name="predicate">The predicate that methods are tested against to see if <typeparamref name="TDecorator"/> should be used</param>
        public void AddDecorator<TDecorator>(Predicate<MethodInfo> predicate) where TDecorator : class, IDecorator {
            Policies.Add(new DecoratorPolicy<TDecorator>(predicate));
        }

        /// <summary>
        /// Adds decorators based on implementations of the <see cref="IDecorateAttribute{TDecorator}"/> interface
        /// </summary>
        public void AddAttributeDecorators() {
            var methodDecorators = typeof(TService)
                .GetMethods()
                .SelectMany(m => m.GetCustomAttributes().Select(a => new {
                    Method = m,
                    Interface = a.GetType().GetInterfaces().SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDecorateAttribute<>))
                }))
                .Where(d => d.Interface != null);

            foreach (var methodDecorator in methodDecorators) {
                addDecoratorMethod.MakeGenericMethod(methodDecorator.Interface.GetGenericArguments()).Invoke(this, new object[] { methodDecorator.Method });
            }
        }
    }
}
