using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Structura.Application.Common.Behaviors;
using Structura.Application.Interfaces;
using Structura.Infrastructure.DbContexts;
using Structura.Infrastructure.Repositories;

namespace Structura.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            if (env.IsDevelopment())
            {
                opt.UseInMemoryDatabase("StructuraDb");
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection") 
                                       ?? throw new InvalidOperationException("Connection string not found!");
                
                opt.UseNpgsql(connectionString);
            }
        });
        
        services.AddScoped<IProductRepository, ProductRepository>();
        
        var applicationAssembly = Assembly.Load("Structura.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}