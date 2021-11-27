# VDT.Core.DependencyInjection

Provides extensions for `Microsoft.Extensions.DependencyInjection.IServiceCollection`

## Features

- Attribute based service registration
- Decorator pattern implementation

## Attribute based service registration

The extension methods `ServiceCollectionExtensions.AddAttributeServices` allow you to move registration for your
services from the `Startup` class to the services themselves. Simply mark your services or service implementations 
with the different types of service attributes to indicate that a service should be registered and call the
appropriate overload to `ServiceCollectionExtensions.AddAttributeServices` register all services in an assembly.

There are six attributes available:
- `TransientServiceAttribute` marks a service to be registered as a transient service with the supplied implementation
type
- `ScopedServiceAttribute` marks a service to be registered as a scoped service with the supplied implementation type
- `SingletoncopedServiceAttribute` marks a service to be registered as a singleton service with the supplied
implementation type
- `TransientServiceImplementationAttribute` marks the implementation to be registered as a transient service under the
supplied service type
- `ScopedServiceImplementationAttribute` marks the implementation to be registered as a scoped service under the
supplied service type
- `SingletonServiceImplementationAttribute` marks the implementation to be registered as a singleton service under the
supplied service type


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
        // Register all classes in an assembly
        services.AddAttributeServices(typeof(Startup).Assembly);

        // Register all classes and apply decorators
        services.AddAttributeServices(typeof(Startup).Assembly, options => options.AddAttributeDecorators());

        // ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // ...
    }
}
```

## Decorators

The extension methods `Decorators.ServiceCollectionExtensions.AddTransient`,
`Decorators.ServiceCollectionExtensions.AddScoped` and `Decorators.ServiceCollectionExtensions.AddSingleton` with an
`Action<Decorators.DecoratorOptions>` parameter allow you to register services decorated with your own implementations
of the `Decorators.IDecorator` interface. This interface has three methods:

- `BeforeExecute` allows you to execute code before execution of the decorated method
- `AfterExecute` allows you to execute code after execution of the decorated method
- `OnError` allows you to execute code if the decorated method throws an error

There are two ways to inject decorators into your services: you can apply a custom attribute to the methods on either
the service or service imlementation you want decorated, or you can provide predicates with decorator types when 
registering the services.

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
        // Register explicitly in Startup
        services.AddScoped<IExample, Example>(options => options.AddDecorator<LogDecorator>(method => method.Name == "Foo");

        // Register using attributes
        services.AddScoped<IExample, Example>(options => options.AddAttributeDecorators());

        // ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // ...
    }
}
```