# VDT.Core.DependencyInjection

Provides extensions for `Microsoft.Extensions.DependencyInjection.IServiceCollection`

## Features

- Attribute based service registration
- Decorator pattern implementation

## Attribute based service registration

The extension methods `ServiceCollectionExtensions.AddAddAttributeServices` allow you to move registration for your
services from the `Startup` class to the services themselves. Simply mark your services with the `[TransientService]`,
`[ScopedService]` and `SingletonService` attributes to indicate that a service should be registered and call the
appropriate overload to `ServiceCollectionExtensions.AddAddAttributeServices` register all services in an assembly.