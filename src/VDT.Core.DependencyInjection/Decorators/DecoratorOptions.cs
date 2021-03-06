using System;
using System.Collections.Generic;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Options to set up decorators to a service
    /// </summary>
    /// <typeparam name="TService">The type of the service to add decorators to</typeparam>
    public sealed class DecoratorOptions<TService> where TService : class {
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
    }
}
