using DddSample.Application.Exercises.Interfaces;
using DddSample.Infrastructure.Behaviors;
using DddSample.Infrastructure.Persistance;
using DddSample.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(o =>
            o.UseSqlite(configuration.GetConnectionString("Default"),
            //show ef wher are migrations
            s => s.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IExerciseReadRepository, ExerciseReadRepository>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EfTransactionBehavior<,>));

            return services;

        }
    }
}
