using System;
using System.Collections.Generic;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    public sealed class DecoratorOptions<TService> where TService : class {
        internal List<DecoratorPolicy> Policies { get; } = new List<DecoratorPolicy>();

        public void AddDecorator<TDecorator>() where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => true);
        }

        public void AddDecorator<TDecorator>(MethodInfo method) where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => m == method);
        }

        public void AddDecorator<TDecorator>(Predicate<MethodInfo> predicate) where TDecorator : class, IDecorator {
            Policies.Add(new DecoratorPolicy<TDecorator>(predicate));
        }
    }
}
