# VDT.Core.DependencyInjection

Provides extensions for `Microsoft.Extensions.DependencyInjection.IServiceCollection`

## Features

- Flexible service registration
- Convention-based service registration
- Attribute-based service registration
- Decorator pattern implementation

## Flexible service registration

The extension method `ServiceCollectionExtensions.AddServices` is used to register all services provided by a `ServiceRegistrationOptions` object built with
the provided setup action. The `ServiceRegistrationOptions` class provides methods to build it in a fluent way:
- `AddAssembly` adds an assembly to scan for services
- `AddAssemblies` adds multiple assemblies to scan for services
  - Overloads are provided to add multiple assemblies as a parameter array or enumerable
  - Overloads are provided to search for assemblies recursively by predicate or assembly name prefix
- `AddServiceTypeProvider` adds a method that returns service types for a given implementation type found in the assemblies
  - These methods must match the delegate method `ServiceTypeProvider`
  - It is possible to supply a `ServiceLifetimeProvider` that returns the appropriate `ServiceLifetime` for a given service type and implementation type
- `UseDefaultServiceLifetime` sets the default `ServiceLifetime` to use if no `ServiceLifetimeProvider` was provided or it did not return a `ServiceLifetime`
- `UseServiceRegistrar` sets the method to use for registering services
  - This method must match the delegate method `ServiceRegistrar`
  - By default a new `ServiceDescriptor` will be created with the found service type, implementation type and lifetime

### Example

```
public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddServices(setupAction: options => options
            // Add assemblies to scan
            .AddAssembly(assembly: typeof(Startup).Assembly)
            .AddAssemblies(typeof(MyService).Assembly, typeof(MyOtherService).Assembly)

            // Add all project assemblies based on prefix
            .AddAssemblies(entryAssembly: typeof(Startup).Assembly, assemblyPrefix: "MyCompany.MySolution")
            
            // Add a service type provider with a lifetime provider
            .AddServiceTypeProvider(
                serviceTypeProvider: implementationType => implementationType.GetInterfaces().Where(serviceType => serviceType.Assembly == implementationType.Assembly),
                serviceLifetimeProvider: (serviceType, implementationType) => ServiceLifetime.Scoped
            )
            
            // Add a service type provider without a lifetime provider
            .AddServiceTypeProvider(
                serviceTypeProvider: implementationType => implementationType.GetInterfaces().Where(serviceType => serviceType.Assembly == implementationType.Assembly)
            )

            // Optional: use a different ServiceLifetime than Scoped by default
            .UseDefaultServiceLifetime(serviceLifetime: ServiceLifetime.Transient)

            // Optional: provide your own method for registering services
            .UseServiceRegistrar(
                serviceRegistrar: (services, serviceType, implementationType, serviceLifetime) => {
                    services.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime);

                    return services;
                }
            )
        );

        // ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // ...
    }
}
```

## Convention-based service registration

The class `DefaultServiceTypeProviders` contains three ways to register services based on common conventions:

- `SingleInterface` returns the single interface if an implementation type implements exactly one interface
- `InterfaceByName` returns interface types found on on implementation types that follow the .NET naming guideline of naming class-interface pairs:
  - Service interface name `IMyService`
  - Implementation class name `MyService`
- `CreateGenericInterfaceTypeProvider` generates a `ServiceTypeProvider` that finds generic types based on a generic type definition
  - This is useful if you implement generic interface types such request handlers, command handlers or query handlers

### Example

```
public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddServices(setupAction: options => options
            // Add assemblies to scan
            .AddAssembly(assembly: typeof(Startup).Assembly)
            .AddAssembly(assembly: typeof(MyService).Assembly)
            
            // Add default service type providers
            .AddServiceTypeProvider(
                serviceTypeProvider: DefaultServiceTypeProviders.SingleInterface
            )
            .AddServiceTypeProvider(
                serviceTypeProvider: DefaultServiceTypeProviders.InterfaceByName
            )
            .AddServiceTypeProvider(
                serviceTypeProvider: DefaultServiceTypeProviders.CreateGenericInterfaceTypeProvider(typeof(IRequestHandler))
            )
        );

        // ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // ...
    }
}
```

## Attribute-based service registration

The extension method `Attributes.ServiceRegistrationOptionsExtensions.AddAttributeServiceTypeProviders` allows you to move registration for your services from
the `Startup` class to the services themselves without using convention-based registration. Simply mark your services or service implementations with the
different types of service attributes to indicate that a service should be registered and use the options extension to add the correct providers.

There are six attributes available:
- `Attributes.TransientServiceAttribute` marks a service to be registered as a transient service with the supplied implementation type
- `Attributes.ScopedServiceAttribute` marks a service to be registered as a scoped service with the supplied implementation type
- `Attributes.SingletoncopedServiceAttribute` marks a service to be registered as a singleton service with the supplied implementation type
- `Attributes.TransientServiceImplementationAttribute` marks the implementation to be registered as a transient service under the supplied service type
- `Attributes.ScopedServiceImplementationAttribute` marks the implementation to be registered as a scoped service under the supplied service type
- `Attributes.SingletonServiceImplementationAttribute` marks the implementation to be registered as a singleton service under the supplied service type

The extension methods `Attributes.ServiceCollectionExtensions.AddAttributeServices` are convenience methods that you can use if you don't need any other
service type providers or additional setup of service registration.

### Example

```

// Mark the service to be registered
[ScopedService(implementationType: typeof(Bar))]
public interface IBar {
    void Foo();
}

public class Bar {
    public void Foo() {
        // ...
    }
}

public interface IBar {
    void Foo();
}

// Mark the implementation
[ScopedServiceImplementation(serviceType: typeof(Bar))]
public class Bar {
    public void Foo() {
        // ...
    }
}

public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        // Register using options
        services.AddServices(setupAction: options => options
            .AddAssembly(assembly: typeof(Startup).Assembly)
            .AddAttributeServiceTypeProviders()
        );

        // Register using convenience method
        services.AddAttributeServices(assembly: typeof(Startup).Assembly);

        // Register using convenience method and apply decorators
        services.AddAttributeServices(assembly: typeof(Startup).Assembly, decoratorSetupAction: options => options.AddAttributeDecorators());

        // ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // ...
    }
}
```

## Decorators

The extension methods `Decorators.ServiceCollectionExtensions.AddTransient`, `Decorators.ServiceCollectionExtensions.AddScoped` and
`Decorators.ServiceCollectionExtensions.AddSingleton` with an `Action<Decorators.DecoratorOptions>` parameter allow you to register services decorated with
your own implementations of the `Decorators.IDecorator` interface.

If you wish to use decorators when using options-based service registration as described above, it is possible to use the 
`Decorators.ServiceRegistrationOptionsExtensions.UseDecoratorServiceRegistrar` extension method which can apply decorator options to all classes that are
registered using the provided `ServiceTypeProvider` implementations.

The `Decorators.IDecorator` interface has three methods:

- `BeforeExecute` allows you to execute code before execution of the decorated method
- `AfterExecute` allows you to execute code after execution of the decorated method
- `OnError` allows you to execute code if the decorated method throws an error

There are two ways to apply decorators to your services: you can apply a custom attribute to the methods on either the service or service imlementation you
want decorated, or you can provide predicates with decorator types when registering the services.

### Example

```
public class LogDecorator : IDecorator {
    public void BeforeExecute(MethodExecutionContext context) {
        Debug.WriteLine($"Executing '{context.TargetType.FullName}.{context.Method.Name}'");
    }

    public void AfterExecute(MethodExecutionContext context) {
        Debug.WriteLine($"Executed '{context.TargetType.FullName}.{context.Method.Name}'");
    }

    public void OnError(MethodExecutionContext context, System.Exception exception) {
        Debug.WriteLine($"Failed to execute '{context.TargetType.FullName}.{context.Method.Name}': {exception.Message}");
    }
}

// For attribute based decoration
[AttributeUsage(AttributeTargets.Method)]
public class LogAttribute : Attribute, IDecorateAttribute<LogDecorator> {
}

public interface IExample {
    // Decorate from service
    [Log]
    void Foo();

    void Bar();
}

public class Example : IExample {
    public void Foo() {
        // ...
    }

    // Decorate from implementation
    [Log]
    public void Bar() {
        // ...
    }
}

public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        // Register services with a provided decorator
        services.AddServices(options => options
            .AddAssembly(assembly: typeof(Startup).Assembly)
            .AddServiceTypeProvider(
                serviceTypeProvider: DefaultServiceTypeProviders.InterfaceByName
            )
            .UseDecoratorServiceRegistrar(setupAction: options => options.AddDecorator<LogDecorator>(predicate: method => method.Name == "Foo")
        );
        
        // Register services using attributes
        services.AddServices(options => options
            .AddAssembly(assembly: typeof(Startup).Assembly)
            .AddServiceTypeProvider(
                serviceTypeProvider: DefaultServiceTypeProviders.InterfaceByName
            )
            .UseDecoratorServiceRegistrar(setupAction: options => options.AddAttributeDecorators())
        );

        // Register a single service with a provided decorator
        services.AddScoped<IExample, Example>(setupAction: options => options.AddDecorator<LogDecorator>(predicate: method => method.Name == "Foo");

        // Register a single service using attributes
        services.AddScoped<IExample, Example>(setupAction: options => options.AddAttributeDecorators());

        // ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // ...
    }
}
```