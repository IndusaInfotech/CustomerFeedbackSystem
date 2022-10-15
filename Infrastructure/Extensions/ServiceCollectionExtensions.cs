using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories;
using Application.Interfaces.Shared;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<Application.Interfaces.Repositories.IProjectRepository, ProjectRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion Repositories
        }
    }
}
