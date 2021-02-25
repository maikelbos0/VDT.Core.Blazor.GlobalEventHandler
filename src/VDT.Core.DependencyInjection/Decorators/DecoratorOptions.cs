using System;
using System.Collections.Generic;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    public sealed class DecoratorOptions<TService> where TService : class {
        private readonly List<DecoratorPolicy> decorators = new List<DecoratorPolicy>();

        public void AddDecorator<TDecorator>() where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => true);
        }

        public void AddDecorator<TDecorator>(MethodInfo method) where TDecorator : class, IDecorator {
            AddDecorator<TDecorator>(m => m == method);
        }

        public void AddDecorator<TDecorator>(Predicate<MethodInfo> shouldInterceptFunc) where TDecorator : class, IDecorator {
            decorators.Add(new DecoratorPolicy<TDecorator>(shouldInterceptFunc));
        }
    }
}
