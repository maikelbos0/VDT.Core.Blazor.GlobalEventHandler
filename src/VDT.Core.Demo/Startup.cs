using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VDT.Core.Demo.Decorators;
using VDT.Core.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using VDT.Core.DependencyInjection.Decorators;

namespace VDT.Core.Demo {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<LogDecorator>();
            services.AddServices(options => options
                .AddAssembly(typeof(Startup).Assembly)
                .AddAttributeServiceTypeProviders()
                .UseDecoratorServiceRegistrar(options => options.AddAttributeDecorators())
            );
            services.AddCors();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors(builder => builder.WithOrigins("https://localhost:44390"));
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
