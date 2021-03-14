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

### Example

```
[ScopedService(typeof(Example))]
public interface IExample {
    void Foo();
}

public class Example {
    public void Foo() {
        // ...
    }
}

public class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddAttributeServices(typeof(Startup).Assembly);
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

- BeforeExecute allows you to execute code before execution of the decorated method
- AfterExecute allows you to execute code after execution of the decorated method
- OnError allows you to execute code if the decorated method throws an error

There are two ways to inject decorators into your services: you can apply a custom attribute to the methods on the
service you want decorated, or you can provide predicates with decorator types when registering the services.

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
    [Log]
    void Foo();
}

public class Example {
    public void Foo() {
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