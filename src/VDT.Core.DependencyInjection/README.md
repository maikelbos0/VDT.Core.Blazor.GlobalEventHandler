# VDT.Core.DependencyInjection

Provides extensions for `Microsoft.Extensions.DependencyInjection.IServiceCollection`

## Features

- Attribute based service registration
- Decorator pattern implementation

## Attribute based service registration

The extension methods `ServiceCollectionExtensions.AddAttributeServices` allow you to move registration for your
services from the `Startup` class to the services themselves. Simply mark your services with the `[TransientService]`,
`[ScopedService]` and `SingletonService` attributes to indicate that a service should be registered and call the
appropriate overload to `ServiceCollectionExtensions.AddAttributeServices` register all services in an assembly.

## Decorators

The extension methods `Decorators.ServiceCollectionExtensions.AddTransient`,
`Decorators.ServiceCollectionExtensions.AddScoped` and `Decorators.ServiceCollectionExtensions.AddSingleton` with an
`Action<Decorators.DecoratorOptions>` parameter allow you to register services decorated with your own implementations
of the `Decorators.IDecorator` interface. This interface has three methods:

- BeforeExecute allows you to execute code before execution of the decorated method
- AfterExecute allows you to execute code after execution of the decorated method
- OnError allows you to execute code if the decorated method throws an error